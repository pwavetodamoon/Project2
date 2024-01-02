using UnityEngine;
// DATA cua nhan vat
[CreateAssetMenu(fileName = "Player Data", menuName = "Scriptable Object/ PLayer")]
public class Player : BaseData
{
    public int level;
    public Transform Slot;
    public WeaponType playerWeaponType;

}
    public enum WeaponType
    {
        Sword, Bow, Stick
    }



