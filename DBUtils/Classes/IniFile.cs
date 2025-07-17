using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

using IniParser;
using IniParser.Model;

namespace DBUtils.Classes
{
    public class IniFile
    {
        private Dictionary<string, Dictionary<string, string>> _iniFileContent;
        private readonly Regex _sectionRegex = new Regex(@"(?<=\[)(?<SectionName>[^\]]+)(?=\])");
        private readonly Regex _keyValueRegex = new Regex(@"(?<Key>[^=]+)=(?<Value>.+)");
        private string FileName;
        private readonly bool Content;

        public IniFile() : this(null) { }

        public IniFile(string FileName, bool Content = false)
        {
            this.FileName = FileName;
            this.Content = Content;
            _iniFileContent = new Dictionary<string, Dictionary<string, string>>();
            if (!string.IsNullOrEmpty(FileName))
                Load(FileName);
        }

        ~IniFile()
        {
            if (_iniFileContent != null)
                _iniFileContent.Clear();
            _iniFileContent = null;
        }

        public string GetValue(string SectionName, string Key, string DefaultValue)
        {
            return (_iniFileContent.ContainsKey(SectionName) && _iniFileContent[SectionName].ContainsKey(Key) ? _iniFileContent[SectionName][Key] : DefaultValue).Trim();
        }

        public List<string> GetSections(string Start)
        {
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(FileName);
            return data.Sections.Where(o => o.SectionName.StartsWith(Start)).Select(o => o.SectionName).ToList();
        }

        public bool IsExistsSection(string SectionName)
        {
            return _iniFileContent.ContainsKey(SectionName);
        }

        public void SetValue(string SectionName, string Key, string Value)
        {
            if (!_iniFileContent.ContainsKey(SectionName))
                _iniFileContent[SectionName] = new Dictionary<string, string>();

            _iniFileContent[SectionName][Key] = Value;
        }

        public bool Load(string FileName)
        {
            if (!Content && !File.Exists(FileName)) return false;

            try
            {
                var Content = this.Content ? FileName.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries) : File.ReadAllLines(FileName);
                _iniFileContent = new Dictionary<string, Dictionary<string, string>>();

                string CurrentSectionName = string.Empty;
                foreach (var Line in Content)
                {
                    Match M = _sectionRegex.Match(Line);
                    if (M.Success)
                    {
                        CurrentSectionName = M.Groups["SectionName"].Value;
                    }
                    else
                    {
                        M = _keyValueRegex.Match(Line);
                        if (M.Success)
                        {
                            var Key = M.Groups["Key"].Value;
                            var value = M.Groups["Value"].Value;

                            Dictionary<string, string> Lista = _iniFileContent.ContainsKey(CurrentSectionName) ? _iniFileContent[CurrentSectionName] : new Dictionary<string, string>();
                            Lista[Key] = value;
                            _iniFileContent[CurrentSectionName] = Lista;
                        }
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Save(string filename)
        {
            var sb = new StringBuilder();
            if (_iniFileContent != null)
                foreach (var sectionName in _iniFileContent)
                {
                    sb.AppendFormat("[{0}]\r\n", sectionName.Key);
                    foreach (var keyValue in sectionName.Value)
                        sb.AppendFormat("{0}={1}\r\n", keyValue.Key, keyValue.Value);
                    sb.Append("\r\n");
                }

            try
            {
                File.WriteAllText(filename, sb.ToString());
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}