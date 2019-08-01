using System.Collections;
using System.Collections.Generic;
using System;
using Newtonsoft.Json;
using UnityEngine;


[Serializable]
public class InventoryClass
{
    [SerializeField] public List<int> characterNum;
    [SerializeField] public List<int> characterClass;
    [SerializeField] public List<string> prefabPath;
    [SerializeField] public List<bool> isOwn;
    [SerializeField] public int shieldItem;
    [SerializeField] public int feverItem;
    [SerializeField] public int rewardItem;
    public InventoryClass()
	{
	}

	public InventoryClass(bool init)
	{
		if (init)
		{
			characterNum = new List<int>();
			characterClass = new List<int>();
			prefabPath = new List<string>();
			isOwn = new List<bool>();
            shieldItem = 0;
            feverItem = 0;
            rewardItem = 0;

            for (int i = 0; i < 22; i++)
            {
                //characterNum, isOwn setting
                characterNum.Add(i);
                if (i == 0)
                {
                    isOwn.Add(true);
                }
                else
                {
                    isOwn.Add(false);
                }


                //charactor class setting
                if (i >= 0 && i < 4)
                {
                    characterClass.Add(0);
                }
                else if (i >= 4 && i < 13)
                {
                    characterClass.Add(1);
                }
                else if (i >= 13 && i < 18)
                {
                    characterClass.Add(2);
                }
                else if (i >= 18 && i < 22)
                {
                    characterClass.Add(3);
                }
            }

            //prefabPath setting.
            prefabPath.Add("prefabs/character/Dog");
            prefabPath.Add("prefabs/character/Duck");
            prefabPath.Add("prefabs/character/Chicken");
            prefabPath.Add("prefabs/character/Chick");
            prefabPath.Add("prefabs/character/Sheep");
            prefabPath.Add("prefabs/character/Goat");
            prefabPath.Add("prefabs/character/Bunny");
            prefabPath.Add("prefabs/character/Cat");
            prefabPath.Add("prefabs/character/Pig");
            prefabPath.Add("prefabs/character/Cow");
            prefabPath.Add("prefabs/character/Deer");
            prefabPath.Add("prefabs/character/Turtle");
            prefabPath.Add("prefabs/character/Lizard");
            prefabPath.Add("prefabs/character/Wolf");
            prefabPath.Add("prefabs/character/Fox");
            prefabPath.Add("prefabs/character/Bird");
            prefabPath.Add("prefabs/character/Bear");
            prefabPath.Add("prefabs/character/Horse");
            prefabPath.Add("prefabs/character/Zebra");
            prefabPath.Add("prefabs/character/Panda");
            prefabPath.Add("prefabs/character/Owl");
            prefabPath.Add("prefabs/character/Lion");
        }
    }
}
