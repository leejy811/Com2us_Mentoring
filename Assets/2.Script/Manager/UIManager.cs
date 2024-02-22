using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region UIComponent
    [Header("Information")]
    public Image hpImage;
    public TextMeshProUGUI coinText;

    [Header("Shop")]
    public TextMeshProUGUI[] ShopPriceText;

    [Header("Stat")]
    public GameObject statWindow;
    public Image[] archerImages;
    public Image[] wizardImages;
    public Image[] knightImages;
    public Image[] priestImages;
    public Image[,] towerImages;
    public TextMeshProUGUI towerTypeText;
    public TextMeshProUGUI towerLevelText;
    public TextMeshProUGUI towerDamageText;
    public TextMeshProUGUI towerSpeedText;
    public TextMeshProUGUI towerRangeText;

    [Header("Enhance")]
    public TextMeshProUGUI enhancePriceText;
    public TextMeshProUGUI sellPriceText;
    public TextMeshProUGUI successProbText;
    public TextMeshProUGUI destroyProbText;
    #endregion

    #region Component
    private PlayerInfo player;
    private DBManager DB;
    private TowerSpawner towerSpawner;
    #endregion

    #region EventFunc
    private void Start()
    {
        player = GameManager.instance.player;
        DB = GameManager.instance.dbManager;
        towerSpawner = GameManager.instance.towerSpawner;
        SetShopInfo();
        InitTowerImage();
    }

    private void Update()
    {
        SetPlayerInfo();
    }
    #endregion

    #region SetValueFunc
    private void InitTowerImage()
    {
        towerImages = new Image[4, 4];

        for(int i = 0;i < archerImages.Length; i++)
        {
            towerImages[0, i] = archerImages[i];
        }
        for (int i = 0; i < wizardImages.Length; i++)
        {
            towerImages[1, i] = wizardImages[i];
        }
        for (int i = 0; i < knightImages.Length; i++)
        {
            towerImages[2, i] = knightImages[i];
        }
        for (int i = 0; i < priestImages.Length; i++)
        {
            towerImages[3, i] = priestImages[i];
        }
    }
    private void SetPlayerInfo()
    {
        hpImage.fillAmount = player.curHealth / player.maxHealth;
        coinText.text = player.coin.ToString();
    }

    private void SetShopInfo()
    {
        for(int i = 0;i < ShopPriceText.Length; i++)
        {
            ShopPriceText[i].text = DB.towerEnhancePrice[0].ToString();
        }
    }

    public void SetStatWindow(Tower pickTower)
    {
        statWindow.SetActive(true);
        SetTowerStat(pickTower);
        SetEnhanceInfo(pickTower);
    }

    public void SetTowerStat(Tower pickTower)
    {
        int towerIndex = (int)pickTower.type;
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                towerImages[i, j].enabled = towerIndex == i && (pickTower.level / 5) == j;
            }
        }
        towerTypeText.text = pickTower.type.ToString();
        towerLevelText.text = (pickTower.level + 1).ToString();
        towerDamageText.text = pickTower.damage.ToString();
        towerSpeedText.text = pickTower.attackSpeed.ToString();
        towerRangeText.text = pickTower.attackRange.ToString();

        if (pickTower.addedDamage > 0)
        {
            towerDamageText.text += " + " + pickTower.addedDamage.ToString();
        }
    }

    public void SetEnhanceInfo(Tower pickTower)
    {
        int level = pickTower.level;
        enhancePriceText.text = "-" + DB.towerEnhancePrice[level].ToString();
        sellPriceText.text = "+" + DB.towerSellPrice[level].ToString();
        successProbText.text = DB.towerSuccessProb[level].ToString() + "% Success";
        destroyProbText.text = DB.towerDestroyProb[level].ToString() + "% Destroy";
    }
    #endregion

    #region OnClickFunc
    public void OnClickBuyButton(int index)
    {
        if (towerSpawner.isBuyTower || towerSpawner.isPickTower) return;
        if (!player.PayCoin(int.Parse(ShopPriceText[index].text)))
            return;

        towerSpawner.BuyTower(index);
    }

    public void OnClickEnhanceButton()
    {
        Tower tower = towerSpawner.curPickTower.GetComponent<Tower>();
        if (tower.level + 1 == DB.towerEnhancePrice.Count) return;
        if (!player.PayCoin(DB.towerEnhancePrice[tower.level])) return;

        if (!tower.LevelUp()) return;

        SetTowerStat(tower);
        SetEnhanceInfo(tower);
    }

    public void OnClickSellButton()
    {
        player.GetCoin(int.Parse(sellPriceText.text));
        Tower tower = towerSpawner.curPickTower.GetComponent<Tower>();
        tower.DestroyTower();
    }

    public void OnClickStatExitButton()
    {
        statWindow.SetActive(false);
        towerSpawner.SetPickTower(false, null);
        towerSpawner.OffPickAllTower();
    }
    #endregion
}