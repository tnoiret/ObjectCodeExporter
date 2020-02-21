using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace ObjectCodeExporter
{
    /// <summary>
    /// Objet de traduction litéral d'une instance d'un objet en vue de sont utilisation pour des tests auto
    /// </summary>
    public class ObjectCodeExporter
    {
        private readonly List<string> _lines = new List<string>();
        private readonly List<string> _objectsPath = new List<string>();
        private int _entityCounter = 0;
        private readonly bool _addTranslatedOnPropertyName;

        /// <summary>
        /// Constructeur
        /// </summary>
        /// <param name="addTranslatedOnPropertyName">Faut-il ajouter "Translated" sur le nom des prop</param>
        public ObjectCodeExporter(bool addTranslatedOnPropertyName = true)
        {
            _addTranslatedOnPropertyName = addTranslatedOnPropertyName;
        }

        /// <summary>
        /// Traduction d'un objet en text d'initialisation de cet objet (traite que les properties)
        /// </summary>
        /// <param name="obj">Objet à exporter</param>
        /// <returns>Le code d'export de l'objet</returns>
        public string Translate(object obj)
        {
            Initialize();
            DumpObject(obj);
            return String.Join(Environment.NewLine, _lines.ToArray()) + Environment.NewLine;
        }

        /// <summary>
        /// Initialisation des propriété
        /// </summary>
        private void Initialize()
        {
            _entityCounter = 0;
            _lines.Clear();
            _objectsPath.Clear();
        }


        /// <summary>
        /// Dump d'un objet
        /// </summary>
        /// <param name="obj">L'objet à dumper</param>
        /// <returns>Le nom de l'objet dumper</returns>
        private string DumpObject(object obj)
        {
            // Récupération du type, de son nom et début de construction
            Type objectType = obj.GetType();
            string currentName = $"{FirstLetterLowCase(objectType.GetRealTypeName().Replace("<", string.Empty).Replace(">", string.Empty))}{GetTranslatedText()}{_entityCounter++}";
            _lines.Add($"{objectType.GetRealTypeName()} {currentName} = new {objectType.GetRealTypeName()}();");

            // Ajout du nom courant dans les chemin, on ajoute et un retire après le dump, pour pouvoir potentiellement gérer un harbo
            _objectsPath.Add(currentName);

            // Dump des propriétés
            DumpProperties(obj, objectType);
            _objectsPath.Remove(currentName);
            return currentName;
        }


        /// <summary>
        /// Retourne le text de translated en fonction du paramétrage utilisateur
        /// </summary>
        /// <returns>Le text du translated ou stirng.empty</returns>
        private string GetTranslatedText()
        {
            if (_addTranslatedOnPropertyName)
                return "Translated";
            else
                return string.Empty;
        }

        /// <summary>
        /// Dump de toutes les properties d'un objet
        /// </summary>
        /// <param name="obj">Objet sur lequel on souhaite récupérer les props</param>
        /// <param name="objectType">Type de l'objet qu'on traite</param>
        private void DumpProperties(object obj, Type objectType)
        {
            if (obj is IEnumerable)
            {
                foreach (var item in (IEnumerable)obj)
                {
                    _lines.Add($"{_objectsPath.Last()}.Add({GetPropertyValue(null, item)});");
                }
            }
            else
            {
                PropertyInfo[] propertyInfos = objectType.GetProperties(GetPropertiesBindingFlagsFilter());
                string currentAccessPath = _objectsPath.Last();
                for (int i = 0; i < propertyInfos.Length; i++)
                {
                    PropertyInfo currentProperty = propertyInfos[i];
                    string value = GetPropertyValue(currentProperty, currentProperty.GetValue(obj));
                    if (!string.IsNullOrEmpty(value))
                        _lines.Add($"{currentAccessPath}.{currentProperty.Name} = {value};");
                }
            }
        }

        /// <summary>
        /// Récupère la valeur d'une propriété d'un objet (ou de tous les items des IEnumerable de type list ou dictionnary)
        /// </summary>
        /// <param name="currentProperty">Propriété courante </param>
        /// <param name="obj">Objet sur lequel on souhaite récupérer les infos</param>
        /// <returns>Soit la valeure directement pour les types simples, soit le nom de l'objet nouvellement créée</returns>
        private string GetPropertyValue(PropertyInfo currentProperty, object obj)
        {
            string result = string.Empty;
            if (obj == null)
            {
                result = "null";
            }
            else if (obj is string)
            {
                result = $"\"{StringHelpers.Escape(obj.ToString())}\"";
            }
            else if (obj is byte || obj is sbyte)
            {
                result = string.Format("0x{0:x2}", obj);
            }
            else if (obj is int || obj is short || obj is uint || obj is ushort)
            {
                result = obj.ToString();
            }
            else if (obj is bool)
            {
                result = obj.ToString().ToLowerInvariant();
            }
            else if (obj is double)
            {
                result = String.Format(CultureInfo.InvariantCulture, "{0}d", obj);
            }
            else if (obj is decimal)
            {
                result = String.Format(CultureInfo.InvariantCulture, "{0}m", obj);
            }
            else if (obj is float)
            {
                result = String.Format(CultureInfo.InvariantCulture, "{0}f", obj);
            }
            else if (obj is long || obj is ulong)
            {
                result = String.Format(CultureInfo.InvariantCulture, "{0}L", obj);
            }
            else if (obj is DateTime dateTime)
            {
                if (dateTime == DateTime.MinValue)
                {
                    result = "DateTime.MinValue";
                }
                else if (dateTime == DateTime.MaxValue)
                {
                    result = "DateTime.MaxValue";
                }
                else
                {
                    result = $"new DateTime({dateTime.Year}, {dateTime.Month}, {dateTime.Day}, {dateTime.Hour}, {dateTime.Minute}, {dateTime.Second}, {dateTime.Millisecond})";
                }
            }
            else if (obj is Enum)
            {

                string fullName = obj.GetType().FullName.Replace('+', '.');
                foreach (var item in ((Enum)obj).GetIndividualFlags())
                {
                    if (!String.IsNullOrEmpty(result))
                        result += '|';
                    result += $"{fullName}.{item}";
                }


            }
            else if (obj is Guid guid)
            {
                result = $"new Guid(\"{guid.ToString()}\")";
            }
            else if (currentProperty != null && currentProperty.PropertyType.IsGenericType && currentProperty.PropertyType.GetGenericTypeDefinition() == typeof(Dictionary<,>) && obj is IEnumerable dicoEnumerable)
            {
                Type itemType = null;
                PropertyInfo dicoKeyProp = null;
                PropertyInfo dicoValueProp = null;
                string currentAccessPath = _objectsPath.Last();
                foreach (var item in dicoEnumerable)
                {
                    if (itemType == null)
                    {
                        itemType = item.GetType();
                        dicoKeyProp = itemType.GetProperty("Key");
                        dicoValueProp = itemType.GetProperty("Value");
                    }
                    _lines.Add($"{currentAccessPath}.{currentProperty.Name}.Add({GetPropertyValue(dicoKeyProp, dicoKeyProp.GetValue(item))}, {GetPropertyValue(dicoValueProp, dicoValueProp.GetValue(item))});");
                }
                // Todo : Gérer ici les List<List<T>> ?
                return string.Empty;
            }
            else if (obj is IEnumerable objEnumerable && currentProperty != null)
            {
                string currentAccessPath = _objectsPath.Last();
                if (currentProperty.SetMethod == null || !currentProperty.SetMethod.IsPrivate)
                {
                    _lines.Add($"{currentAccessPath}.{currentProperty.Name} = new {currentProperty.PropertyType.GetRealTypeName()}();");
                }

                // If set are private (like CA XXXXX) need do not create instance
                foreach (var item in objEnumerable)
                {
                    // Todo en attendant de gérer les types génériques
                    if (!(item is string) && item is IEnumerable)
                    {
                        _lines.Add("Cannot dump IEnumerable<IEnumerable<T>> for now sorry");
                        return string.Empty;
                    }
                    _lines.Add($"{currentAccessPath}.{currentProperty.Name}.Add({GetPropertyValue(currentProperty, item)});");
                }
                return string.Empty;
            }
            // No result ? Maybe a custom type ! Translate It !
            if (string.IsNullOrEmpty(result))
            {
                result = DumpObject(obj);
            }

            return result;
        }

        /// <summary>
        /// Retourn le filtre de flag pour les bindings
        /// </summary>
        /// <returns>Un enum flags des Binding à gérer pour la rélfexion</returns>
        private BindingFlags GetPropertiesBindingFlagsFilter()
        {
            return BindingFlags.Instance | BindingFlags.Public;
        }

        /// <summary>
        /// Return string with first letter to lower
        /// </summary>
        /// <param name="str">string</param>
        /// <returns>string with first char lowered</returns>
        private string FirstLetterLowCase(string str)
        {
            return char.ToLower(str[0], CultureInfo.CurrentCulture) + str.Substring(1);
        }
    }
}
