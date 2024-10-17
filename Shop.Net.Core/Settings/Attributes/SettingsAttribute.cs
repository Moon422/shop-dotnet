using System;
using System.IO;

namespace Shop.Net.Core.Settings.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
public class SettingsAttribute : Attribute
{
    readonly string fileName;
    readonly string fullPath;

    public SettingsAttribute(string fileName)
    {
        this.fileName = fileName;
        fullPath = Path.Join(Environment.CurrentDirectory, "Settings", fileName);
    }

    public string Filename => fileName;

    public string FullPath => fullPath;
}