using UnityEngine;

// DATA cua nhan vat
[CreateAssetMenu(fileName = "CharactersData Data", menuName = "Scriptable Object/ PLayer")]
public class CharactersData : BaseData
{
    public int level;
    public Transform Slot;
    public WeaponType playerWeaponType;
}

public enum WeaponType
{
    Sword, Bow, Stick
}