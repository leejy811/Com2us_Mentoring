using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset]
public class DataBase : ScriptableObject
{
	public List<TowerEnhanceDBEntities> TowerEntities;
	public List<TowerInfoDBEntities> ArcherEntities;
	public List<TowerInfoDBEntities> WizardEntities;
	public List<TowerInfoDBEntities> KnightEntities;
	public List<TowerInfoDBEntities> PriestEntities;
}
