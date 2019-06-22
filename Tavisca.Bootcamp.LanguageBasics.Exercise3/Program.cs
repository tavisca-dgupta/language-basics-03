using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Tavisca.Bootcamp.LanguageBasics.Exercise1
{
    public static class Program
    {
        static void Main(string[] args)
        {
            Test(
                new[] { 3, 4 }, 
                new[] { 2, 8 }, 
                new[] { 5, 2 }, 
                new[] { "P", "p", "C", "c", "F", "f", "T", "t" }, 
                new[] { 1, 0, 1, 0, 0, 1, 1, 0 });
            Test(
                new[] { 3, 4, 1, 5 }, 
                new[] { 2, 8, 5, 1 }, 
                new[] { 5, 2, 4, 4 }, 
                new[] { "tFc", "tF", "Ftc" }, 
                new[] { 3, 2, 0 });
            Test(
                new[] { 18, 86, 76, 0, 34, 30, 95, 12, 21 }, 
                new[] { 26, 56, 3, 45, 88, 0, 10, 27, 53 }, 
                new[] { 93, 96, 13, 95, 98, 18, 59, 49, 86 }, 
                new[] { "f", "Pt", "PT", "fT", "Cp", "C", "t", "", "cCp", "ttp", "PCFt", "P", "pCt", "cP", "Pc" }, 
                new[] { 2, 6, 6, 2, 4, 4, 5, 0, 5, 5, 6, 6, 3, 5, 6 });
            Console.ReadKey(true);
        }

        private static void Test(int[] protein, int[] carbs, int[] fat, string[] dietPlans, int[] expected)
        {
            var result = SelectMeals(protein, carbs, fat, dietPlans).SequenceEqual(expected) ? "PASS" : "FAIL";
            Console.WriteLine($"Proteins = [{string.Join(", ", protein)}]");
            Console.WriteLine($"Carbs = [{string.Join(", ", carbs)}]");
            Console.WriteLine($"Fats = [{string.Join(", ", fat)}]");
            Console.WriteLine($"Diet plan = [{string.Join(", ", dietPlans)}]");
            Console.WriteLine(result);
        }

         public static int[] SelectMeals(int[] protein, int[] carbs, int[] fat, string[] dietPlans)
        {
             int[] itemNumber=new int[dietPlans.Count()];
             int[] calories=new int[protein.Count()];
             int[] itemToEat=new int[protein.Count()];// store the number of all the items

             for(int i=0;i<calories.Count();i++)
             {
                 calories[i]=(protein[i]*5)+(carbs[i]*5)+(fat[i]*9);
                 itemToEat[i]=i;//as 1st your will have all the items to eat
             }
             for(int i=0;i<dietPlans.Count();i++)
             {
                 if(dietPlans[i].Equals(""))
                 {
                     itemNumber[i]=0;
                 }
                 else
                 {
                     char[] dietarray=dietPlans[i].ToCharArray();
                     int[] item=getItemNumber(dietarray[0],protein,carbs,fat,calories,itemToEat).ToArray();
                     if(dietarray.Count()==1)
                     {
                         itemNumber[i]=item[0];
                     }
                     else{
                     for(int j=0;j<dietarray.Count();j++)
                     {
                         item=getItemNumber(dietarray[j],protein,carbs,fat,calories,item).ToArray();                         
                         if(item.Count()==1||j+1==dietarray.Count())
                         {
                             itemNumber[i]=item[0];
                             break;
                         }
                     }
                     }
                 }
             }
             return itemNumber;
         }
             
        public static List<int> getItemNumber(char dietPlan,int[] protein, int[] carbs, int[] fat,int[] calories,int[] itemToEat)
        {
            List<int> itemConfig = new List<int>(); //go get the configuration of items a user can eat as per diet plan
            switch(dietPlan)
            {
                case 'P':itemConfig=getItemConfig(itemToEat,protein);
                return findItems(protein,itemConfig.Max());
                break;
                case 'p':itemConfig=getItemConfig(itemToEat,protein);
                return findItems(protein,itemConfig.Min());
                break;
                case 'C':itemConfig=getItemConfig(itemToEat,carbs);
                return findItems(carbs,itemConfig.Max());
                break;
                case 'c': itemConfig=getItemConfig(itemToEat,carbs);
                return findItems(carbs,itemConfig.Min());
                break;
                case 'F': itemConfig=getItemConfig(itemToEat,fat);
                return findItems(fat,itemConfig.Max());
                break;
                case 'f':itemConfig=getItemConfig(itemToEat,fat);
                return findItems(fat,itemConfig.Min());
                break;
                case 'T':itemConfig=getItemConfig(itemToEat,calories);
                return findItems(calories,itemConfig.Max());
                break;
                case 't':itemConfig=getItemConfig(itemToEat,calories);
                return findItems(calories,itemConfig.Min());
                break;
                default: return null;
                
            }
        }
        public static List<int> findItems(int[] array,int val)
        {
            List<int> itemNumber = new List<int>(); 
            for(int i=0;i<array.Length;i++){
                if(array[i]==val)
                {
                    itemNumber.Add(i);
                }
            }
            return itemNumber;
        }
        
        public static List<int> getItemConfig(int[] itemToEat,int[] array)
        {
            List<int> item = new List<int>(); 
            for(int i=0;i<itemToEat.Count();i++)
            {
                item.Add(array[itemToEat[i]]);                

            }
            return item;
        }
    }
}
