// See https://aka.ms/new-console-template for more information

using POC.Localization.Framework;
using POC.Localization.Plugin1;
using System.Reflection;

PropertyInfo prop = typeof(CToto).GetProperty(nameof(CToto.Parameter1_AAAA))!;

var attr = prop.GetCustomAttribute<CLocalizedPropertyAttribute>();
Console.WriteLine(attr!.ResourceKey);