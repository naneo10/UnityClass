using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EquipMent : MonoBehaviour
{
    #region field
    //리스트에 추가/변경하는 식으로 장비 착용 및 변경
    public static EquipMent Instance { get; private set; }

    public List<ItemDataSO> equipment;

    [Header("인벤토리 UI")]
    public Inventory inventory;

    [Header("장비 슬롯")]
    [SerializeField] EquipSlot helmet;
    [SerializeField] EquipSlot armor;
    [SerializeField] EquipSlot gloves;
    [SerializeField] EquipSlot pants;
    [SerializeField] EquipSlot shoes;
    [SerializeField] EquipSlot weapone;
    #endregion

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        FreshList();
    }

    #region method
    public void AddEquipement(ItemDataSO item)
    {
        //조건식의 값을 담을 변수
        ItemDataSO duplicate = equipment.Find(x =>
            (x.helmet && item.helmet) ||
            (x.armor && item.armor) ||
            (x.gloves && item.gloves) ||
            (x.pants && item.pants) ||
            (x.shoes && item.shoes) ||
            (x.weapone && item.weapone)
        );

        //장비 타입 중복 여부 확인
        if (duplicate == null)
        {
            equipment.Add(item);
            inventory.items.Remove(item);

            inventory.FreshSlot();
            FreshList();
            
            EquipState(item);
        }
        //장비 타입이 중복일 경우 기존 장비는 인벤토리로
        else if (duplicate != null)
        {
            Change(item, duplicate);
        }
    }

    public void Change(ItemDataSO item, ItemDataSO duplicate)
    {
        inventory.AddItem(duplicate); //착용하고 있던 장비 다시 인벤토리로
        inventory.RemoveItem(item); //착용되는 장비는 인벤토리에서 제거

        equipment.Remove(duplicate); //기존 착용된 장비는 제거
        equipment.Add(item); //새로 착용된 장비 추가

        FreshList();
        inventory.FreshSlot();

        ChangeState(item, duplicate);
    }

    public void FreshList()
    {
        //for문 안에 if, else if로 작성했으나 배열의 인덱스 번호가 바뀌면서
        //앞에서 할당되었던 데이터가 null값으로 덮어 씌워지는 결과가 발생
        //https://twd0622.tistory.com/95
        //https://dragontory.tistory.com/343
        //해당 항목만 확인해서 값 유무 갱신
        helmet.Item = equipment.Find(x => x.helmet);
        armor.Item = equipment.Find(x => x.armor);
        gloves.Item = equipment.Find(x => x.gloves);
        pants.Item = equipment.Find(x => x.pants);
        shoes.Item = equipment.Find(x => x.shoes);
        weapone.Item = equipment.Find(x => x.weapone);
    }

    public void RemoveEquipment(ItemDataSO item)
    {
        inventory.items.Add(item);
        equipment.Remove(item);

        FreshList();
        inventory.FreshSlot();

        EquipState(item);
    }

    //Any : https://kwonyeeun.tistory.com/127
    private void EquipState(ItemDataSO item)
    {
        if (item.armor)
        {
            bool equippedArmor = equipment.Any(x => x.armor);

            if (equippedArmor)
            {
                PlayerStatus.Instance().defense += item.defense;
                Debug.Log($"현재 방어력 : {PlayerStatus.Instance().defense}");
            }
            else
            {
                PlayerStatus.Instance().defense -= item.defense;
                Debug.Log($"현재 방어력 : {PlayerStatus.Instance().defense}");
            }
        }

        if (item.weapone)
        {
            bool equippedWeapone = equipment.Any(x => x.weapone);

            if (equippedWeapone)
            {
                PlayerStatus.Instance().damage += item.damage;
                Debug.Log($"현재 공격력 : {PlayerStatus.Instance().damage}");
            }
            else
            {
                PlayerStatus.Instance().damage -= item.damage;
                Debug.Log($"현재 공격력 : {PlayerStatus.Instance().damage}");
            }
        }
    }

    private void ChangeState(ItemDataSO item, ItemDataSO duplicate)
    {
        if (item.armor && duplicate.armor)
        {
            PlayerStatus.Instance().defense -= duplicate.defense;
            PlayerStatus.Instance().defense += item.defense;
            Debug.Log($"현재 방어력 : {PlayerStatus.Instance().defense}");
        }

        if (item.weapone && duplicate.weapone)
        {
            PlayerStatus.Instance().damage -= duplicate.damage;
            PlayerStatus.Instance().damage += item.damage;
            Debug.Log($"현재 공격력 : {PlayerStatus.Instance().damage}");
        }
    }
    #endregion
}