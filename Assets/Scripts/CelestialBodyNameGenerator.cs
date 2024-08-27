using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CelestialBodyNameGenerator
{
    private static List<string> planetNames = new List<string>
    {
        "Aetheris",
        "Bellatrix",
        "Celestia",
        "Delphius",
        "Elysium",
        "Fornax",
        "Galaxia",
        "Helios",
        "Ignis",
        "Jovis",
        "Kronos",
        "Lyra",
        "Meridian",
        "Nova",
        "Orionis",
        "Pyxis",
        "Seraphis",
        "Taurus",
        "Ursa",
        "Vespera"
    };

    public static string GeneratePlanetName()
    {
        int index = Random.Range(0, planetNames.Count);
        return planetNames[index] + " " + GeneratePlanetCode();
    }

    public static string GeneratePlanetCode()
    {
        const string allowedCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        string code = "";

        for (int i = 0; i < 4; i++)
        {
            int index = Random.Range(0, allowedCharacters.Length);
            code += allowedCharacters[index];
        }

        return code;
    }
}
