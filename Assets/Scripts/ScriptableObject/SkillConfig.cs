using TMPro;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill System/SkillConfig")]
public class SkillConfig : ScriptableObject
{
    [SerializeField]  private string _skillName;
    [SerializeField]  private Sprite _iconSkill;
    [SerializeField]  private Image _imageCoolDown;
    [SerializeField]  private TextMeshProUGUI _coolDownText;
    [SerializeField]  private bool _canUse = true;
  
  public string GetSkillName()
  {
    return _skillName;
  }
  public void SetSkillName(string name)
  {
     _skillName = name;
  }
  //
  public Sprite GetIconSkill()
  {
      return _iconSkill;
  }
  public void SetIconSkill(Image image)
  {
      _iconSkill = image.sprite;
  }
  //
  
  public Image GetImageCoolDown()
  {
      return _imageCoolDown;
  }
  public void SetImageCoolDown(Image image)
  {
      _imageCoolDown = image;
  }
  //
  
  public TextMeshProUGUI GetCoolDownText()
  {
      return _coolDownText;
  }
  public void SetCoolDownText(TextMeshProUGUI text)
  {
      _coolDownText.text = text.text;
  }
  //
  
  public bool GetIsCanUse()
  {
      return _canUse;
  }
  public void SetIsCanUse(bool value)
  {
      _canUse = value;
  }
  //
}

