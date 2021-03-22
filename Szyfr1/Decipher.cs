using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Szyfr1 {
    class Decipher {
        private string EncodedText { get; set; }
        private string Key { get; set; }
        private List<int> RightSkew { get; set; }
        private List<int> LeftSkew { get; set; }

        public Decipher(string text, string k, List<int> rs, List<int> ls) {
            EncodedText = text;
            Key = k;
            RightSkew = rs;
            LeftSkew = ls;
            RightSkew.Sort();
            LeftSkew.Sort();
        }

        private char?[] FillWithNulls(char?[] array) {
            for (int i = 0; i < array.Length; i++) {
                array[i] = null;
            }
            return array;
        }

        public string Decode() {
            Cipher c = new Cipher();
            char?[] text = new char?[EncodedText.Length];
            Dictionary<int, char> numberedKey = new Dictionary<int, char>();
            int blocks;
            int counter = 0;
            text = FillWithNulls(text);
            numberedKey = c.SortKey(Key);
            blocks = (EncodedText.Length / numberedKey.Count) / 5;

            for (int blockNumber = 0; blockNumber <= blocks; blockNumber++) {
                foreach (int skewNumber in RightSkew) {
                    for (int keyNumber = 0; keyNumber < numberedKey.Count; keyNumber++) {
                        if (numberedKey.Keys.ElementAt(keyNumber) == skewNumber) {
                            try {
                                if (text[(keyNumber) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + (numberedKey.Count + 1)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + (numberedKey.Count + 1)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count + 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count + 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count + 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count + 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count + 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count + 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                            }
                            catch (IndexOutOfRangeException) { continue; }
                        }
                    }
                }
                foreach (int skewNumber in LeftSkew) {
                    for (int keyNumber = 0; keyNumber < numberedKey.Count; keyNumber++) {
                        if (numberedKey.Keys.ElementAt(keyNumber) == skewNumber) {
                            try {
                                if (text[(keyNumber) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + (numberedKey.Count - 1)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + (numberedKey.Count - 1)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count - 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count - 1) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count - 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count - 1) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count - 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count - 1) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                            }
                            catch (IndexOutOfRangeException) { continue; }
                        }

                    }
                }
                for (int number = 0; number <= numberedKey.Count; number++) {
                    for (int keyNumber = 0; keyNumber < numberedKey.Keys.Count; keyNumber++) {
                        if (number == numberedKey.Keys.ElementAt(keyNumber)) {
                            try {
                                if (text[(keyNumber) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + (numberedKey.Count)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + (numberedKey.Count)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count) * 2)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count) * 3)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                                if (text[(keyNumber + ((numberedKey.Count) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] == null) {
                                    text[(keyNumber + ((numberedKey.Count) * 4)) + (5 * numberedKey.Count * (blocks - (blocks - blockNumber)))] = EncodedText[counter];
                                    counter++;
                                }
                            }
                            catch (IndexOutOfRangeException) { continue; }
                        }

                    }
                }

            }
            string result = "";
            foreach (char? ca in text) {
                if (ca == null) continue;
                else result += ca;
            }
            return result;
        }
    }
}
