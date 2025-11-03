using System.Collections.Generic;
using UnityEngine;

public class EquipMent : MonoBehaviour
{
    #region field
    //리스트에 추가/변경하는 식으로 장비 착용 및 변경
    public List<ItemDataSO> equipment;

    [Header("인벤토리 UI")]
    public Inventory inventory;

    [Header("장비 슬롯")]
    [SerializeField] Slot helmet;
    [SerializeField] Slot armor;
    [SerializeField] Slot gloves;
    [SerializeField] Slot pants;
    [SerializeField] Slot shoes;
    [SerializeField] Slot weapone;
    #endregion

    void Awake()
    {
        for (int i = 0; i < equipment.Count; i++)
        {
            Debug.Log($"equipment[{i}] : {equipment[i].name}");
        }
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

        Debug.Log($"현재 duplicate에 담긴 값{duplicate}");

        //장비 타입 중복 여부 확인
        if (duplicate == null)
        {
            equipment.Add(item);
            inventory.items.Remove(item);
            inventory.FreshSlot();
            FreshList();
        }
        //장비 타입이 중복일 경우 기존 장비는 인벤토리로
        else if (duplicate != null)
        {
            Change(item, duplicate);
            FreshList();
            inventory.FreshSlot();
        }
    }

    public void Change(ItemDataSO item, ItemDataSO duplicate)
    {
        inventory.items.Add(duplicate); //착용하고 있던 장비 다시 인벤토리로
        inventory.items.Remove(item); //착용되는 장비는 인벤토리에서 제거
        equipment.Remove(duplicate); //기존 착용된 장비는 제거
        equipment.Add(item); //새로 착용된 장비 추가
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
    }
    #endregion
}