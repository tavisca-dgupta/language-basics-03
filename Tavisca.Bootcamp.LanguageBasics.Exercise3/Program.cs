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
             int[] calories=GetCalories(protein,carbs,fat);
             int[] itemToEat=GetInitialItemToEat(protein);// store the number of all the items
             for(int i=0;i<dietPlans.Count();i++)
             {
                 if(dietPlans[i].Equals(""))
                 {
                     itemNumber[i]=0;
                 }
                 else
                 {
                     char[] dietarray=dietPlans[i].ToCharArray();
                     int[] item=GetItemNumber(dietarray[0],protein,carbs,fat,calories,itemToEat).ToArray();
                     if(dietarray.Count()==1)
                     {
                         itemNumber[i]=item[0];
                     }
                     else{
                        for(int j=0;j<dietarray.Count();j++)
                        {
                            item=GetItemNumber(dietarray[j],protein,carbs,fat,calories,item).ToArray();                         
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
             
        public static List<int> GetItemNumber(char dietPlan,int[] protein, int[] carbs, int[] fat,int[] calories,int[] itemToEat)
        {
            List<int> itemConfig = new List<int>(); //go get the configuration of items a user can eat as per diet plan
            switch(dietPlan)
            {
                case 'P':itemConfig = GetItemConfig(itemToEat,protein);
                return FindItems(protein,itemConfig.Max());
                case 'p':itemConfig = GetItemConfig(itemToEat,protein);
                return FindItems(protein,itemConfig.Min());
                case 'C':itemConfig = GetItemConfig(itemToEat,carbs);
                return FindItems(carbs,itemConfig.Max());
                case 'c': itemConfig = GetItemConfig(itemToEat,carbs);
                return FindItems(carbs,itemConfig.Min());
                case 'F': itemConfig = GetItemConfig(itemToEat,fat);
                return FindItems(fat,itemConfig.Max());
                case 'f':itemConfig = GetItemConfig(itemToEat,fat);
                return FindItems(fat,itemConfig.Min());
                case 'T':itemConfig = GetItemConfig(itemToEat,calories);
                return FindItems(calories,itemConfig.Max());
                case 't':itemConfig = GetItemConfig(itemToEat,calories);
                return FindItems(calories,itemConfig.Min());
                default: return null;
                
            }
        }
        public static List<int> FindItems(int[] array,int val)
        {
            List<int> itemNumber = new List<int>(); 
            for(int i=0;i<array.Length;i++)
            {
                if(array[i]==val)
                {
                    itemNumber.Add(i);
                }
            }
            return itemNumber;
        }
        
        public static List<int> GetItemConfig(int[] itemToEat,int[] array)
        {
            List<int> item = new List<int>(); 
            for(int i=0;i<itemToEat.Count();i++)
            {
                item.Add(array[itemToEat[i]]);                
            }
            return item;
        }
		
		public static int[] GetCalories(int[] protein,int[] carbs,int[] fat)
		{
			int[] calories=new int[protein.Count()];
			for(int i=0;i<calories.Count();i++)
            {
                 calories[i]=(protein[i]*5)+ (carbs[i]*5)+ (fat[i]*9);
            }
			return calories;
		}
		
		public static int[] GetInitialItemToEat(int[] protein)
		{	
			int[] itemToEat=new int[protein.Count()];
			for(int i=0;i<itemToEat.Count();i++)
            {
                itemToEat[i]=i;//as 1st your will have all the items to eat
            }
			return itemToEat;
		}
    }
}
