using System;
using System.IO;

namespace compiler
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read the text file and saves it in a string
            string text = File.ReadAllText("sample1.txt");
            //Declare scanner string
            string scanner = "";
            //Declare tokens string
            string tokens = "";
            //identifiersTable string to save identifiers found
            string identifiersTable = "";
            //valuesTable string to save numbers found
            string valuesTable = "";
            //Errors string
            string errors = "";
            //Temporal string to save identifiers and numbers
            string temporal = "";

            //Get Tokens IDs Table
            TokenIdsTable tokensTable = new TokenIdsTable();
            string[] tokenIdsTable = tokensTable.setTokens();

            //Integer variables to keep control of the program
            int i = -1;
            int entryId = 1;
            int entryNum = 1;

            //boolean values that work as helpers for detecting errors
            bool readingComment = false;
            bool temporalIsUnvalid = false;

            //string arrays to save symbol tables
            string[] symbolTableIds = { };
            string[] symbolTableNums = { };

            //convert all text to an array of chars
            char[] chars = text.ToCharArray();
            foreach (char character in chars)
            {
                i++;
                if (i < chars.Length - 1)
                {
                    if (Char.IsLetter(character) && readingComment != true)
                    {
                        //keep saving letters in a temporal value until a delimiter is found
                        if (Char.IsWhiteSpace(chars[i + 1]) != true && chars[i + 1] != '[' && chars[i + 1] != ']' && chars[i + 1] != ';' && chars[i + 1] != ',' && chars[i + 1] != '(' && chars[i + 1] != ')' && chars[i + 1] != '{' && chars[i + 1] != '}' && chars[i + 1] != '<' && chars[i + 1] != '>' && chars[i + 1] != '=' && chars[i + 1] != '!' && chars[i + 1] != '*' && chars[i + 1] != '/' && chars[i + 1] != '+' && chars[i + 1] != '-')
                        {
                            temporal += character.ToString();
                            //check if letters and digits are not mixed
                            if (Char.IsDigit(chars[i + 1]) || Char.IsDigit(chars[i - 1])
                                
                                )
                            {
                                temporalIsUnvalid = true;
                            }
                        }
                        // check if it is a keyword or an identifier and save the token id and updatessymbol table if necessary 
                        else
                        {
                            temporal += character.ToString();
                            //check if letters and digits are not mixed
                            if (Char.IsDigit(chars[i + 1]) || Char.IsDigit(chars[i - 1]))
                            {
                                temporalIsUnvalid = true;
                            }
                            string temporalUpper = temporal.ToUpper();
                            switch (temporalUpper)
                            {
                                case "ELSE":
                                    {
                                        scanner += ";21";
                                        break;
                                    }
                                case "IF":
                                    {
                                        scanner += ";22";
                                        break;
                                    }
                                case "INT":
                                    {
                                        scanner += ";33";
                                        break;
                                    }
                                case "RETURN":
                                    {
                                        scanner += ";44";
                                        break;
                                    }
                                case "VOID":
                                    {
                                        scanner += ";55";
                                        break;
                                    }
                                case "WHILE":
                                    {
                                        scanner += ";66";
                                        break;
                                    }
                                case "INPUT":
                                    {
                                        scanner += ";77";
                                        break;
                                    }
                                case "OUTPUT":
                                    {
                                        scanner += ";88";
                                        break;
                                    }
                                default:
                                    {
                                        bool alreadyInTable = false;
                                        int index = 0;
                                        foreach (string symbol in symbolTableIds)
                                        {
                                            if (temporal == symbol)
                                            {
                                                alreadyInTable = true;
                                                break;
                                            }
                                            index++;
                                        }
                                        if (alreadyInTable && !temporalIsUnvalid)
                                        {
                                            scanner += ";123," + index;
                                        }
                                        else if(!temporalIsUnvalid)
                                        {
                                            identifiersTable += ';' + temporal.ToString();
                                            symbolTableIds = identifiersTable.Split(';');
                                            scanner += ";123," + entryId.ToString();
                                            entryId++;
                                        }
                                        else
                                        {
                                            errors += (";Can't identify '" + temporal + "' as integer or identifier");
                                            temporalIsUnvalid = false;
                                        }
                                        break;
                                    }
                            }
                            temporal = "";
                        }
                    }
                    //keep saving digits in a temporal value until a delimiter is found
                    else if (Char.IsDigit(character) && readingComment != true)
                    {
                        if (Char.IsWhiteSpace(chars[i + 1]) != true && chars[i + 1] != '[' && chars[i + 1] != ']' && chars[i + 1] != ';' && chars[i + 1] != ',' && chars[i + 1] != '(' && chars[i + 1] != ')' && chars[i + 1] != '{' && chars[i + 1] != '}' && chars[i + 1] != '<' && chars[i + 1] != '>' && chars[i + 1] != '=' && chars[i + 1] != '!' && chars[i + 1] != '*' && chars[i + 1] != '/' && chars[i + 1] != '+' && chars[i + 1] != '-')
                        {
                            temporal += character.ToString();
                        }
                        //save the token id and update symbol table if necessary 
                        else
                        {
                            temporal += character.ToString();
                            bool alreadyInTable = false;
                            int index = 0;
                            foreach (string symbol in symbolTableNums)
                            {
                                if (temporal == symbol) {
                                    alreadyInTable = true;
                                    break;
                                }
                                index++;
                            }
                            if (alreadyInTable && !temporalIsUnvalid)
                            {
                                scanner += ";456," + index;

                            } else if (!temporalIsUnvalid)
                            {
                                valuesTable += ';' + temporal.ToString();
                                symbolTableNums = valuesTable.Split(';');
                                scanner += ";456," + entryNum;
                                entryNum++;
                            }
                            else
                            {
                                errors += (";Can't identify '" + temporal + "' as integer or identifier");
                                temporalIsUnvalid = false;
                            }
                            temporal = "";
                        }
                    }
                    //recognize a whitespace and ignore it
                    else if (Char.IsWhiteSpace(character))
                    {
                        continue;
                    }
                    else
                    {
                        //switch to identify token (and some errors) and save it's id in the scanner string
                        switch (character)
                        {
                            case '/':
                                if (chars[i + 1] == '*')
                                {
                                    readingComment = true;
                                    /*scanner += ";501";*/
                                    break;
                                }
                                else if (chars[i - 1] == '*')
                                {
                                    break;
                                }
                                else
                                {
                                    scanner += ";4";
                                    break;
                                }
                            case '*':
                                if (chars[i + 1] == '/')
                                {
                                    if (readingComment == true) scanner += ";503";
                                    else 
                                    {
                                        errors += "*/ is missing opening comment symbol '/*'";
                                        scanner += ";503";
                                    }
                                    readingComment = false;
                                    /*scanner += ";502";*/
                                    break;
                                }
                                else if (chars[i - 1] == '/')
                                {
                                    break;
                                }
                                else
                                {
                                    if (readingComment != true) scanner += ";3";
                                    break;
                                }
                            case '<':
                                if (chars[i + 1] == '=')
                                {
                                    if (readingComment != true) scanner += ";6";
                                    break;
                                }
                                else
                                {
                                    if (readingComment != true) scanner += ";5";
                                    break;
                                }
                            case '>':
                                if (chars[i + 1] == '=')
                                {
                                    if (readingComment != true) scanner += ";8";
                                    break;
                                }
                                else
                                {
                                    if (readingComment != true) scanner += ";7";
                                    break;
                                }
                            case '=':
                                if (chars[i + 1] == '=')
                                {
                                    if (readingComment != true) scanner += ";9";
                                    break;
                                }
                                else if (chars[i - 1] == '<' || chars[i - 1] == '>' || chars[i - 1] == '!' || chars[i - 1] == '=')
                                {
                                    break;
                                }
                                else
                                {
                                    if (readingComment != true) scanner += ";11";
                                    break;
                                }
                            case '!':
                                if (chars[i + 1] == '=')
                                {
                                    if (readingComment != true) scanner += ";10";
                                    break;
                                }
                                else if (readingComment != true)
                                {
                                    errors += ";Character not valid: " + character;
                                    break;
                                } else
                                {
                                    break;
                                }
                            case '+':
                                if (readingComment != true) scanner += ";1";
                                break;
                            case '-':
                                if (readingComment != true) scanner += ";2";
                                break;
                            case ';':
                                if (readingComment != true) scanner += ";100";
                                break;
                            case ',':
                                if (readingComment != true) scanner += ";200";
                                break;
                            case '(':
                                if (readingComment != true) scanner += ";300";
                                break;
                            case ')':
                                if (readingComment != true) scanner += ";400";
                                break;
                            case '[':
                                if (readingComment != true) scanner += ";500";
                                break;
                            case ']':
                                if (readingComment != true) scanner += ";600";
                                break;
                            case '{':
                                if (readingComment != true) scanner += ";700";
                                break;
                            case '}':
                                if (readingComment != true) scanner += ";800";
                                break;
                            default: //if a character is not a valid token, letter, digit, or white space, it is an invalid character
                                if (readingComment != true) 
                                {
                                    errors += ";Character not valid: " + character;
                                }
                                break;
                        }
                    }
                }
            }
            //check if al comments are closed and save the error if necessary
            if (readingComment == true)
            {
                errors += ";'*/' expected to close comment";
            }
            Console.WriteLine();
            Console.WriteLine("Scanner Output");
            Console.WriteLine("--------------");
            i = 0;
            //split scanner string into an array of token ids
            string[] scannerOutput = scanner.Split(';');
            //print scanner output
            foreach (string id in scannerOutput)
            {
                if (i > 0) Console.WriteLine("   <" + id + ">");
                i++;
            }
            i = 0;
            //look for the corresponding token, id or number for each element of the scanner and save values in the tokens string
            foreach (string tokenId in scannerOutput)
            {
                string[] separator = tokenId.Split(',');
                if (separator.Length == 1)
                {
                    if (i > 0)
                    {
                        int id = Int32.Parse(separator[0]);
                        tokens += '%' + tokenIdsTable[id];
                    }

                } else if (separator[0] == "123")
                {
                    int entry = Int32.Parse(separator[1]);
                    tokens += '%' + symbolTableIds[entry];
                } else
                {
                    int entry = Int32.Parse(separator[1]);
                    tokens += '%' + symbolTableNums[entry];
                }
                i++;
            }

            //split tokens string into an array of tokens
            string[] tokenRecognition = tokens.Split('%');
            Console.WriteLine();
            Console.WriteLine("Tokens Recognition");
            Console.WriteLine("------------------");
            i = 0;
            //print tokens recognition
            foreach (string token in tokenRecognition)
            {
                if (i > 0) Console.WriteLine("      " + token);
                i++;
            }
            Console.WriteLine();
            Console.WriteLine(" Entry |    ID");
            Console.WriteLine("-------------------");
            //print identifiers symbol table
            for (i = 1; i < symbolTableIds.Length; i++)
            {
                Console.WriteLine("   " + i + "   |   "+ symbolTableIds[i]);
            }
            Console.WriteLine("-------------------");
            Console.WriteLine();
            Console.WriteLine("  Entry |  Integer");
            Console.WriteLine("-------------------");
            //print numbers symbol table
            for (i = 1; i < symbolTableNums.Length; i++)
            {
                Console.WriteLine("    " + i + "   |   " + symbolTableNums[i]);
            }
            //print errors found
            Console.WriteLine();
            Console.WriteLine("       Errors      ");
            Console.WriteLine("-------------------");
            string[] errorsTable = errors.Split(';');
            foreach(string error in errorsTable)
            {
                Console.WriteLine(error);
            }
            Console.WriteLine();
        }
    }
}

