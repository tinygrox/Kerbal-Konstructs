﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using KerbalKonstructs.Core;
using KerbalKonstructs.UI;


namespace KerbalKonstructs.UI2
{
    class GrassEditor
    {
        internal static PopupDialog dialog;
        internal static MultiOptionDialog optionDialog;
        internal static List<DialogGUIBase> content;

        internal static string windowName = "GrassEditor";
        internal static string windowMessage = null;
        internal static string windowTitle = "Grass Editor";

        internal static Rect windowRect;

        //internal static float windowWidth = Screen.width * 0.9f;
        internal static float windowWidth = 300f;
        internal static float windowHeight = 300f;

        internal static bool showTitle = false;
        internal static bool showKKTitle = true;
        internal static bool isModal = true;


        internal static bool placeToParent = false;
        internal static bool checkForParent = true;

        internal static Func<bool> parentWindow = EditorGUI.instance.IsOpen;

        internal static GrassColor2 selectedMod = null;

        internal static string NearGrassTexture;
        internal static string FarGrassTexture;
        internal static string TamarcTexture;
        internal static string BlendMaskTexture;

        internal static string NearGrassTiling;
        internal static string FarGrassTiling;
        internal static string BlendDistance;

        internal static StaticInstance staticInstance = null;


        internal static void CreateContent()
        {
            content.Add(new DialogGUIHorizontalLayout(
                new DialogGUILabel("All Models", HighLogic.UISkin.label),
                new DialogGUIFlexibleSpace()
                ));


            content.Add(VaiantList);
            content.Add(new DialogGUIVerticalLayout(
                new DialogGUILabel("NearGrassTexture", HighLogic.UISkin.label),
                new DialogGUITextInput(NearGrassTexture, NearGrassTexture, false, 40, SetString, 25),
                new DialogGUILabel("FarGrassTexture", HighLogic.UISkin.label),
                new DialogGUITextInput(FarGrassTexture, FarGrassTexture, false, 40, SetString, 25),
                new DialogGUILabel("TamarcTexture", HighLogic.UISkin.label),
                new DialogGUITextInput(TamarcTexture, TamarcTexture, false, 40, SetString, 25),
                new DialogGUILabel("BlendMaskTexture", HighLogic.UISkin.label),
                new DialogGUITextInput(BlendMaskTexture, BlendMaskTexture, false, 40, SetString, 25),
                new DialogGUIHorizontalLayout(
                    new DialogGUILabel(" Grass: R ", HighLogic.UISkin.label),
                    new DialogGUISlider(GetGrassFloatR, 0, 1, false, 140, 25, SetGrassFloatR),
                    new DialogGUITextInput("0", false, 10, SetGrassRStr, GetGrassRStr, TMPro.TMP_InputField.ContentType.DecimalNumber , 25)),
               new DialogGUIHorizontalLayout(
                    new DialogGUILabel(" Grass: G ", HighLogic.UISkin.label),
                    new DialogGUISlider(GetGrassFloatG, 0, 1, false, 140, 25, SetGrassFloatG),
                    new DialogGUITextInput("0", false, 10, SetGrassGStr, GetGrassGStr, TMPro.TMP_InputField.ContentType.DecimalNumber, 25)),
               new DialogGUIHorizontalLayout(
                    new DialogGUILabel(" Grass: B ", HighLogic.UISkin.label),
                    new DialogGUISlider(GetGrassFloatB, 0, 1, false, 140, 25, SetGrassFloatB),
                    new DialogGUITextInput("0", false, 10, SetGrassBStr, GetGrassBStr, TMPro.TMP_InputField.ContentType.DecimalNumber, 25)),
               new DialogGUIHorizontalLayout(
                    new DialogGUILabel("Tarmac: R ", HighLogic.UISkin.label),
                    new DialogGUISlider(GetTarmacFloatR, 0, 1, false, 140, 25, SetTarmacFloatR),
                    new DialogGUITextInput("0", false, 10, SetTarmacRStr, GetTarmacRStr, TMPro.TMP_InputField.ContentType.DecimalNumber, 25)),
               new DialogGUIHorizontalLayout(
                    new DialogGUILabel("Tarmac: G ", HighLogic.UISkin.label),
                    new DialogGUISlider(GetTarmacFloatG, 0, 1, false, 140, 25, SetTarmacFloatG),
                    new DialogGUITextInput("0", false, 10, SetTarmacGStr, GetTarmacGStr, TMPro.TMP_InputField.ContentType.DecimalNumber, 25)),
               new DialogGUIHorizontalLayout(
                    new DialogGUILabel("Tarmac: B ", HighLogic.UISkin.label),
                    new DialogGUISlider(GetTarmacFloatB, 0, 1, false, 140, 25, SetTarmacFloatB),
                    new DialogGUITextInput("0", false, 10, SetTarmacBStr, GetTarmacBStr, TMPro.TMP_InputField.ContentType.DecimalNumber, 25)),
               new DialogGUIHorizontalLayout(
                    new DialogGUILabel("NearGrassTiling", HighLogic.UISkin.label),
                    new DialogGUITextInput(NearGrassTiling, NearGrassTiling, false, 10, SetNearTile, 25)),
               new DialogGUIHorizontalLayout(
                    new DialogGUILabel("FarGrassTiling", HighLogic.UISkin.label),
                    new DialogGUITextInput(FarGrassTiling, FarGrassTiling, false, 10, SetFarTile, 25)),
                new DialogGUIHorizontalLayout(
                    new DialogGUILabel("FarBlendDistance", HighLogic.UISkin.label),
                    new DialogGUITextInput(BlendDistance, BlendDistance, false, 10, SetBlendDistance, 25))

                )); ;
        }



        internal static string SetString(string teststring)
        {
            Log.Normal("SetString: " + teststring);
            return teststring;
        }

        internal static string SetNearTile(string newTile)
        {
            selectedMod.nearGrassTiling = float.Parse(newTile);
            selectedMod.SetColor();
            return newTile;
        }

        internal static string SetFarTile(string newTile)
        {
            selectedMod.farGrassTiling = float.Parse(newTile);
            selectedMod.SetColor();
            return newTile;
        }
        internal static string SetBlendDistance(string newTile)
        {
            selectedMod.farGrassBlendDistance = float.Parse(newTile);
            selectedMod.SetColor();
            return newTile;
        }


        //red
        internal static float GetGrassFloatR()
        {
            return selectedMod.grassColor.r;
        }
        internal static string GetGrassRStr()
        {
            return selectedMod.grassColor.r.ToString();
        }
        internal static void SetGrassFloatR(float newFloat)
        {
            selectedMod.grassColor.r = newFloat;
            selectedMod.SetColor();
        }

        internal static string SetGrassRStr(string newFloat)
        {
            selectedMod.grassColor.r = float.Parse(newFloat);
            selectedMod.SetColor();
            return newFloat;
        }

        //Green
        internal static float GetGrassFloatG()
        {
            return selectedMod.grassColor.g;
        }
        internal static string GetGrassGStr()
        {
            return selectedMod.grassColor.g.ToString();
        }

        internal static void SetGrassFloatG(float newFloat)
        {
            selectedMod.grassColor.g = newFloat;
            selectedMod.SetColor();
        }

        internal static string SetGrassGStr(string newFloat)
        {
            selectedMod.grassColor.r = float.Parse(newFloat);
            selectedMod.SetColor();
            return newFloat;
        }

        //blue
        internal static float GetGrassFloatB()
        {
            return selectedMod.grassColor.b;
        }
        internal static string GetGrassBStr()
        {
            return selectedMod.grassColor.b.ToString();
        }
        internal static void SetGrassFloatB(float newFloat)
        {
            selectedMod.grassColor.b = newFloat;
            selectedMod.SetColor();
        }
        internal static string SetGrassBStr(string newFloat)
        {
            selectedMod.grassColor.r = float.Parse(newFloat);
            selectedMod.SetColor();
            return newFloat;
        }

        // Tarmac
        //red
        internal static float GetTarmacFloatR()
        {
            return selectedMod.tarmacColor.r;
        }
        internal static string GetTarmacRStr()
        {
            return selectedMod.tarmacColor.r.ToString();
        }
        internal static void SetTarmacFloatR(float newFloat)
        {
            selectedMod.tarmacColor.r = newFloat;
            selectedMod.SetColor();
        }

        internal static string SetTarmacRStr(string newFloat)
        {
            selectedMod.tarmacColor.r = float.Parse(newFloat);
            selectedMod.SetColor();
            return newFloat;
        }

        //Green
        internal static float GetTarmacFloatG()
        {
            return selectedMod.tarmacColor.g;
        }
        internal static string GetTarmacGStr()
        {
            return selectedMod.tarmacColor.g.ToString();
        }

        internal static void SetTarmacFloatG(float newFloat)
        {
            selectedMod.tarmacColor.g = newFloat;
            selectedMod.SetColor();
        }

        internal static string SetTarmacGStr(string newFloat)
        {
            selectedMod.tarmacColor.r = float.Parse(newFloat);
            selectedMod.SetColor();
            return newFloat;
        }

        //blue
        internal static float GetTarmacFloatB()
        {
            return selectedMod.tarmacColor.b;
        }
        internal static string GetTarmacBStr()
        {
            return selectedMod.tarmacColor.b.ToString();
        }
        internal static void SetTarmacFloatB(float newFloat)
        {
            selectedMod.tarmacColor.b = newFloat;
            selectedMod.SetColor();
        }
        internal static string SetTarmacBStr(string newFloat)
        {
            selectedMod.tarmacColor.r = float.Parse(newFloat);
            selectedMod.SetColor();
            return newFloat;
        }



        internal static DialogGUIScrollList VaiantList
        {
            get
            {
                List<DialogGUIBase> list = new List<DialogGUIBase>();
                //list.Add(new DialogGUIContentSizer(ContentSizeFitter.FitMode.Unconstrained, ContentSizeFitter.FitMode.PreferredSize, true));
                //list.Add(new DialogGUIFlexibleSpace());
                //list.Add(new DialogGUIButton("Default", delegate { SetVariant(null);}, 140.0f, 30.0f, true));

                foreach (var grass in EditorGUI.selectedInstance.mesh.GetComponents<GrassColor2>())
                {
                    list.Add(new DialogGUIButton(grass.GrassMeshName, delegate { SelectGrass(grass); }, delegate { return (grass != selectedMod); }, 140.0f, 25.0f, false));
                }
                list.Add(new DialogGUIFlexibleSpace());
                var layout = new DialogGUIVerticalLayout(10, 100, 4, new RectOffset(6, 24, 10, 10), TextAnchor.MiddleCenter, list.ToArray());
                var scroll = new DialogGUIScrollList(new Vector2(200, 50), new Vector2(200, 25f * list.Count), false, false, layout);
                return scroll;
            }
        }


        internal static void KKTitle()
        {
            if (!showKKTitle)
            {
                return;
            }
            content.Add(new DialogGUIHorizontalLayout(
                new DialogGUILabel("-KK-", KKStyle.windowTitle),
                new DialogGUIFlexibleSpace(),

                new DialogGUILabel(windowTitle, KKStyle.windowTitle),
                new DialogGUIFlexibleSpace(),
                new DialogGUIButton("X", delegate { Close(); }, 21f, 21.0f, true, KKStyle.DeadButtonRed)

                ));
        }





        internal static void CreateMultiOptionDialog()
        {
            windowRect = new Rect(WindowManager.GetPosition(windowName), new Vector2(windowWidth, windowHeight));
            optionDialog = new MultiOptionDialog(windowName, windowMessage, windowTitle, null, windowRect, content.ToArray());

        }


        internal static void CreatePopUp()
        {
            dialog = PopupDialog.SpawnPopupDialog(new Vector2(0.5f, 0.5f),
                   new Vector2(0.5f, 0.5f), optionDialog,
                   false,
                   null, isModal);
            if (!showTitle)
            {
                dialog.gameObject.GetChild("Title").SetActive(false);
            }
            if (checkForParent)
            {
                dialog.dialogToDisplay.OnUpdate += CheckForParent;
            }
            if (placeToParent)
            {
                dialog.dialogToDisplay.OnUpdate += PlaceToParent;
            }

        }

        internal static void PlaceToParent()
        {

        }


        internal static void CheckForParent()
        {
            if (checkForParent)
            {
                if (parentWindow != null && !parentWindow.Invoke())
                {
                    Close();
                }
                if (staticInstance != EditorGUI.selectedInstance )
                {
                    Close();
                }
            }
        }




        internal static void Open()
        {
            KKStyle.Init();
            staticInstance = EditorGUI.selectedInstance;
            staticInstance.HighlightObject(Color.clear);
            selectedMod = EditorGUI.selectedInstance.mesh.GetComponents<GrassColor2>()[0];
            ReadMod(selectedMod);
            //windowRect = new Rect(CreateBesidesMainwindow(), new Vector2(windowWidth, windowHeight));
            content = new List<DialogGUIBase>();
            KKTitle();
            CreateContent();
            CreateMultiOptionDialog();
            CreatePopUp();

        }


        internal static void Close()
        {
            if (EditorGUI.instance.IsOpen() && (EditorGUI.selectedInstance == staticInstance))
            {
                staticInstance.HighlightObject(XKCDColors.Green_Yellow);
            }

            if (dialog != null)
            {
                WindowManager.SavePosition(dialog);
                dialog.Dismiss();
            }
            
            dialog = null;
            optionDialog = null;

        }


        internal static bool isOpen
        {
            get
            {
                return (dialog != null);
            }
        }

        internal static void Toggle()
        {
            if (isOpen)
            {
                Close();
            }
            else
            {
                Open();
            }
        }

        internal static void ReadMod(GrassColor2 mod)
        {
            if (mod == null)
            {
                Log.UserError("No Grass Mod Found");
                return; 
            }
            NearGrassTexture = mod.nearGrassTexture != null ? mod.nearGrassTextureName : "no texture";
            FarGrassTexture = mod.farGrassTexture != null ? mod.farGrassTextureName : "no texture";
            TamarcTexture = mod.tarmacTexture != null ? mod.tarmacTextureName : "no texture";
            BlendMaskTexture = mod.blendMaskTexture != null ? mod.blendMaskTextureName : "no texture";
            NearGrassTiling = mod.nearGrassTiling.ToString();
            FarGrassTiling = mod.farGrassTiling.ToString();
            BlendDistance = mod.farGrassBlendDistance.ToString();
        }


        internal static void SelectGrass(GrassColor2 grass)
        {
            selectedMod = grass;
            ReadMod(selectedMod);
        }

    }
}
