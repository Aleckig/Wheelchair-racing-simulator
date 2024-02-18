using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Text;
using System.Security.Cryptography;

namespace LevelManagement.Data
{
   // The class responsible for saving and loading game data in JSON format
    public class JsonSaver
    {
        // The filename for saving and loading data
        private static readonly string filename = "saveData1.sav";

        // Get the full path of the save file
        public static string GetSaveFilename()
        {
            return Application.persistentDataPath + "/" + filename;
        }

        // Save method that takes a SaveData object as input
        public void Save(SaveData data)
        {
            data.hashValue = String.Empty;
            // Convert the SaveData object to a JSON string
            string json = JsonUtility.ToJson(data);
            string hashString = GetSHA256(json);
            json = JsonUtility.ToJson(data);

            // Get the full path of the save file
            string saveFilename = GetSaveFilename();

            // Open a file stream for writing
            FileStream fileStream = new FileStream(saveFilename, FileMode.Create);

            // Use a StreamWriter to write the JSON string to the file
            using (StreamWriter writer = new StreamWriter(fileStream))
            {
                writer.Write(json);
            }

            // The file stream will be closed automatically when leaving this block
        }

        // Load method that takes a SaveData object as input and returns a boolean indicating success
        public bool Load(SaveData data)
        {
            // Get the full path of the save file
            string loadFilename = GetSaveFilename();

            // Check if the file exists
            if (File.Exists(loadFilename))
            {
                // Open a file stream for reading
                using (StreamReader reader = new StreamReader(loadFilename))
                {
                    // Read the entire content of the file as a JSON string
                    string json = reader.ReadToEnd();

                    //Check if the hash value is correct
                    if(CheckData(json))
                    {
                        // Deserialize the JSON string into the provided SaveData object
                        JsonUtility.FromJsonOverwrite(json, data);
                    }
                    else
                    {
                        Debug.LogWarning("Integrity check failed. The data may be corrupted.");
                    }

                    
                }

                // Return true indicating successful loading
                return true;
            }

            // Return false indicating that the file does not exist
            return false;
        }
        // This method checks the integrity of a JSON string by comparing its hash values before and after modification.

        // Define a private method named CheckData that takes a string parameter 'json'.
        private bool CheckData(string json)
        {
            // Create a new instance of the SaveData class to store temporary data.
            SaveData tempSaveData = new SaveData();

            // Deserialize the input JSON string and overwrite the contents of tempSaveData.
            JsonUtility.FromJsonOverwrite(json, tempSaveData);

            // Store the original hash value from tempSaveData in the variable 'oldHash'.
            string oldHash = tempSaveData.hashValue;

            // Clear the hashValue property in tempSaveData to prepare for re-calculation.
            tempSaveData.hashValue = string.Empty;

            // Serialize tempSaveData to JSON without the hashValue property to create a modified JSON string.
            string tempJson = JsonUtility.ToJson(tempSaveData);

            // Calculate the SHA256 hash of the modified JSON string.
            string newHash = GetSHA256(tempJson);

            // Compare the original hash value with the newly calculated hash value.
            // If they match, the data integrity is considered intact, and the method returns true.
            return (oldHash == newHash);
        }


        // Delete method to delete the save file
        public void Delete()
        {
            // Delete the save file
            File.Delete(GetSaveFilename());
        }
        public string GetHexStringFromHash(byte[] hash)
        {
            string hexString = string.Empty;
            foreach (byte b in hash)
            {
                hexString += b.ToString("x2");
            }
            return hexString;
        }

        // This method calculates the SHA256 hash of a given text and returns the result as a hexadecimal string.

        // Define a private method named GetSHA256 that takes a string parameter 'text'.
        private string GetSHA256(string text)
        {
            // Convert the input text to a byte array using UTF-8 encoding.
            byte[] textToBytes = Encoding.UTF8.GetBytes(text);

            // Create an instance of SHA256Managed to perform the SHA256 hashing.
            SHA256Managed mySHA256 = new SHA256Managed();

            // Compute the hash value of the byte array using SHA256.
            byte[] hashValue = mySHA256.ComputeHash(textToBytes);

            // Convert the byte array hash value to a hexadecimal string.
            // The GetHexStringFromHash method is assumed to be implemented elsewhere.
            return GetHexStringFromHash(hashValue);
        }

    }

}
