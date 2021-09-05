using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UMA;
using UMA.CharacterSystem;
using UnityEngine.UI;

public class CharacterCreatorScript : MonoBehaviour
{
    public DynamicCharacterAvatar dynamicCharacterAvatarScript;
    public Dictionary<string, DnaSetter> dNAmodule;
    
    
    [Header("Male and female Race names")]

    public string maleRaceName;
    public string femaleRaceName;

    [Space(10)]

    [Header("DNA module related data")]

    public List<Slider> DNAModuleSliders;
    public List<string> DNAModuleStrings;

    public int dNAModuleValue;
    [Space(10)]

    [Header("Color module related data")]
    
    public List<string> ColorModuleStrings;

    public int ColorChangerValue;
    [Space(10)]


    #region recipies

    public List<string> chestRecipieNames;

    #endregion

    #region GenderChanges

    public void ChangeGender(bool genderState)
    {
        if (genderState && dynamicCharacterAvatarScript.activeRace.name != maleRaceName)
        {
            dynamicCharacterAvatarScript.ChangeRace(maleRaceName);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
        else if(!genderState && dynamicCharacterAvatarScript.activeRace.name!=femaleRaceName)
        {
            dynamicCharacterAvatarScript.ChangeRace(femaleRaceName);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
    }

    private void OnEnable()
    {
        dynamicCharacterAvatarScript.CharacterUpdated.AddListener(UpdateCheck);
    }

    private void OnDisable()
    {
        dynamicCharacterAvatarScript.CharacterUpdated.RemoveListener(UpdateCheck);
    }

    void UpdateCheck(UMAData data)
    {
        dNAmodule = dynamicCharacterAvatarScript.GetDNA();
    }
    #endregion



    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetChestRecipie();
        }

        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            ClearChestRecipe();
        }
    }

    public void SetChestRecipie()
    {
        dynamicCharacterAvatarScript.SetSlot("Chest", "MaleHoodie_Recipe");
        dynamicCharacterAvatarScript.BuildCharacter();
    }

    public void ClearChestRecipe()
    {
        dynamicCharacterAvatarScript.ClearSlot("Chest");
        dynamicCharacterAvatarScript.BuildCharacter();
    }


    #region DNAModule


    public void DNAValueChanged(int valueDNA)
    {
        dNAModuleValue = valueDNA;
    }

    public void DNAModuleAdjustments()
    {
        ChangeDNAModuleParameter(dNAModuleValue);
    }

    public void ChangeDNAModuleParameter(int valueDNA)
    {
        if (dNAmodule == null)
        {
            Debug.Log("no Dna module found!");
            dNAmodule = dynamicCharacterAvatarScript.GetDNA();
        }
        else
        {
            dNAmodule[DNAModuleStrings[valueDNA]].Set(DNAModuleSliders[valueDNA].value);
            dynamicCharacterAvatarScript.BuildCharacter();
        }
    }


    #endregion


    #region Colors Module

    

    public void SkinColorChanged()
    {
        ColorChangerValue = 0;
    }

    public void ColorModuleAdjustments(Color col)
    {
        switch (ColorChangerValue)
        {
            case 0:
                Debug.Log("Changing Colors");
                dynamicCharacterAvatarScript.SetColor(ColorModuleStrings[ColorChangerValue], col);
                dynamicCharacterAvatarScript.UpdateColors(true);

                break;

        }
    }
    #endregion

}
