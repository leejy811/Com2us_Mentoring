using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBManager : MonoBehaviour
{
    public DataBase DB;

    public List<int> towerEnhancePrice = new List<int>();
    public List<int> towerSellPrice = new List<int>();
    public List<float> towerSuccessProb = new List<float>();
    public List<float> towerDestroyProb = new List<float>();

    public List<int>[] towerAttackDamage = new List<int>[4];
    public List<float>[] towerAttackSpeed = new List<float>[4];
    public List<float>[] towerAttackRange = new List<float>[4];

    private void Awake()
    {
        for(int i = 0; i < towerAttackDamage.Length; i++)
        {
            towerAttackDamage[i] = new List<int>();
            towerAttackSpeed[i] = new List<float>();
            towerAttackRange[i] = new List<float>();
        }

        LoadEnhanceDB();
        LoadInfoDB();
    }

    private void LoadEnhanceDB()
    {
        List<TowerEnhanceDBEntities> entities = DB.TowerEntities;

        foreach(TowerEnhanceDBEntities entity in entities)
        {
            towerEnhancePrice.Add(entity.EnhancePrice);
            towerSellPrice.Add(entity.SellPrice);
            towerSuccessProb.Add(entity.SuccessProb);
            towerDestroyProb.Add(entity.DestroyProb);
        }
    }

    private void LoadInfoDB()
    {
        List<List<TowerInfoDBEntities>> entities2D = new List<List<TowerInfoDBEntities>>();
        entities2D.Add(DB.ArcherEntities);
        entities2D.Add(DB.WizardEntities);
        entities2D.Add(DB.KnightEntities);
        entities2D.Add(DB.PriestEntities);

        for (int i = 0;i < entities2D.Count;i++)
        {
            foreach (TowerInfoDBEntities entity in entities2D[i])
            {
                towerAttackDamage[i].Add(entity.Damage);
                towerAttackSpeed[i].Add(entity.Speed);
                towerAttackRange[i].Add(entity.Range);
            }
        }
    }
}
