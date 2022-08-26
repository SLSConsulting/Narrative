﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Word;
using System.IO;
using System.Windows.Forms;
using DocumentFormat.OpenXml;

namespace findandreplacenarr
{
    public partial class Form1 : Form
    {
        List <Panel> listPanel = new List <Panel> ();
        int panelIndex;
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        //Find and Replace Method
        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object ToFindText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllforms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref ToFindText,
                ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundLike,
                ref nmatchAllforms, ref forward,
                ref wrap, ref format, ref replaceWithText,
                ref replace, ref matchKashida,
                ref matchDiactitics, ref matchAlefHamza,
                ref matchControl);
        }

        //Find Text and Replace with Image Method
        private void FindTextAndReplaceImage(Microsoft.Office.Interop.Word.Application wordApp, Microsoft.Office.Interop.Word.Document myWordDoc, string textToFind, string imgLocation)
        {
            // Find text and replace with image
            Microsoft.Office.Interop.Word.Find fnd = wordApp.ActiveWindow.Selection.Find;
            fnd.ClearFormatting();
            fnd.Replacement.ClearFormatting();
            fnd.Forward = true;
            fnd.Wrap = Microsoft.Office.Interop.Word.WdFindWrap.wdFindContinue;

            string imagePath = imgLocation;
            var keyword = textToFind;
            var sel = wordApp.Selection;
            sel.Find.Text = string.Format("[{0}]", keyword);
            wordApp.Selection.Find.Execute(keyword);

            Microsoft.Office.Interop.Word.Range range = wordApp.Selection.Range;
            if (range.Text.Contains(keyword))
            {
                //gets desired range here it gets last character to make superscript in range 
                Microsoft.Office.Interop.Word.Range temprange = myWordDoc.Range(range.End - 7, range.End);//keyword is of 4 charecter range.End - 4
                temprange.Select();
                Microsoft.Office.Interop.Word.Selection currentSelection = wordApp.Selection;
                //currentSelection.Font.Superscript = 1;

                sel.Find.Execute(Replace: WdReplace.wdReplaceOne);
                sel.Range.Select();
                var imagePath1 = Path.GetFullPath(string.Format(imagePath, keyword));
                sel.InlineShapes.AddPicture(FileName: imagePath1, LinkToFile: false, SaveWithDocument: true);
            }
        }

        //Fire Separation Distance Find and Replace Function
        private void FSDFindAndReplace(string Findfsd, string FindfsdRating, string FindfsdOpening, int input, Microsoft.Office.Interop.Word.Application wordApp, string FindfsdOccupancy, string buildingType)
        {
            int IntFSDInput = input;
            string noccupancy = FindfsdOccupancy;
            string FSDRating;
            string FSDOpening;
            if (IntFSDInput > 0 && IntFSDInput < 3)
            {
                FSDOpening = "Not Permitted";
                this.FindAndReplace(wordApp, FindfsdOpening, FSDOpening);
                this.FindAndReplace(wordApp, Findfsd, IntFSDInput);
                if (noccupancy == "F-1" || noccupancy == "M" || noccupancy == "S-1")
                {
                    FSDRating = "2";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
                else
                {
                    FSDRating = "1";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }

            }
            else if (IntFSDInput >= 3 && IntFSDInput < 5)
            {
                FSDOpening = "15%";
                this.FindAndReplace(wordApp, FindfsdOpening, FSDOpening);
                this.FindAndReplace(wordApp, Findfsd, IntFSDInput);
                if (noccupancy == "F-1" || noccupancy == "M" || noccupancy == "S-1")
                {
                    FSDRating = "2";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
                else
                {
                    FSDRating = "1";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
            }
            else if (IntFSDInput >= 5 && IntFSDInput < 10)
            {
                FSDOpening = "25%";
                this.FindAndReplace(wordApp, FindfsdOpening, FSDOpening);
                this.FindAndReplace(wordApp, Findfsd, IntFSDInput);
                if (noccupancy == "F-1" || noccupancy == "M" || noccupancy == "S-1")
                {
                    if (buildingType == "Type IA")
                    {
                        FSDRating = "2";
                        this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                    }
                    else
                    {
                        FSDRating = "1";
                        this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                    }
                }
                else
                {
                    FSDRating = "1";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
            }
            else if (IntFSDInput >= 10 && IntFSDInput < 15)
            {
                FSDOpening = "45%";
                this.FindAndReplace(wordApp, FindfsdOpening, FSDOpening);
                this.FindAndReplace(wordApp, Findfsd, IntFSDInput);
                if (buildingType == "Type IIB" || buildingType == "Type VB")
                {

                    FSDRating = "0";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
                else
                {
                    FSDRating = "1";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
            }
            else if (IntFSDInput >= 15 && IntFSDInput < 20)
            {
                FSDOpening = "75%";
                this.FindAndReplace(wordApp, FindfsdOpening, FSDOpening);
                this.FindAndReplace(wordApp, Findfsd, IntFSDInput);
                if (buildingType == "Type IIB" || buildingType == "VB")
                {

                    FSDRating = "0";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
                else
                {
                    FSDRating = "1";
                    this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
                }
            }
            else if(IntFSDInput >= 20)
            {
                FSDOpening = "No Limit";
                this.FindAndReplace(wordApp, FindfsdOpening, FSDOpening);
                this.FindAndReplace(wordApp, Findfsd, IntFSDInput);
                FSDRating = "0";
                this.FindAndReplace(wordApp, FindfsdRating, FSDRating);
            }

        }


        //Create DOC
        private void CreateWordDocument(object filename, object SaveAs)
        {
            Microsoft.Office.Interop.Word.Application wordApp = new Microsoft.Office.Interop.Word.Application();
            //object missing = Missing.Value;
            Microsoft.Office.Interop.Word.Document myWordDoc = null;


            if (File.Exists((string)filename))
            {
                object readOnly = false;
                object isVisible = true;
                wordApp.Visible = true;

                myWordDoc = wordApp.Documents.Open(ref filename, ref readOnly);

                myWordDoc.Activate();
                

                //find and replace
                this.FindAndReplace(wordApp, "PNAME", ProjectNameInput.Text);
                this.FindAndReplace(wordApp, "PADDRESS", ProjectAddressInput.Text);
                this.FindAndReplace(wordApp, "PCITY", ProjectCityInput.Text);
                this.FindAndReplace(wordApp, "PSTATE", ProjectStateInput.Text);
                this.FindAndReplace(wordApp, "PZIPCODE", ProjectZipcodeInput.Text);
                this.FindAndReplace(wordApp, "ARCH", AccountNameInput.Text);
                this.FindAndReplace(wordApp, "ARCHADD", AccountAddressInput.Text);
                this.FindAndReplace(wordApp, "ARCHZIP", AccountZipcodeInput.Text);
                this.FindAndReplace(wordApp, "PNUMBER", ProjectNumberInput.Text);
                this.FindAndReplace(wordApp, "DATE", DateInput.Text);

                string buildingTypeHeight = null;
                bool VOA = false;
                bool VOB = false;
                bool VOM = false;
                bool VOR1 = false;
                bool VOR2 = false;
                bool VOI1 = false;
                bool VOI3 = false;
                bool VOS = false;
                int numVOLevels = VOLevelsInput.Text.Count(c => char.IsDigit(c) && c != ',');

                if (occupancyClassificationInput.CheckedItems.Contains("A"))
                {
                    VOA = true;
                }
                if (occupancyClassificationInput.CheckedItems.Contains("B"))
                {
                    VOB = true;
                }
                if (occupancyClassificationInput.CheckedItems.Contains("M"))
                {
                    VOM = true;
                }
                if (occupancyClassificationInput.CheckedItems.Contains("R-1"))
                {
                    VOR1 = true;
                }
                if (occupancyClassificationInput.CheckedItems.Contains("R-2"))
                {
                    VOR2 = true;
                }
                if (occupancyClassificationInput.CheckedItems.Contains("I-1"))
                {
                    VOI1 = true;
                }
                if (occupancyClassificationInput.CheckedItems.Contains("I-3"))
                {
                    VOI3 = true;
                }
                if (occupancyClassificationInput.CheckedItems.Contains("S"))
                {
                    VOS = true;
                }


                int intBuildingHeight = int.Parse(BuildingHeightInput.Text);

                bool A1 = false;
                bool A2 = false;
                bool A3 = false;
                bool B = false;
                bool M = false;
                bool R1 = false;
                bool R2 = false;
                bool I1 = false;
                bool I3 = false;
                bool S1 = false;
                bool S2 = false;
                A1 = BuildingOccupancyListBox.GetItemChecked(0);
                A2 = BuildingOccupancyListBox.GetItemChecked(1);
                A3 = BuildingOccupancyListBox.GetItemChecked(2);
                B = BuildingOccupancyListBox.GetItemChecked(3);
                M = BuildingOccupancyListBox.GetItemChecked(4);
                R1 = BuildingOccupancyListBox.GetItemChecked(5);
                R2 = BuildingOccupancyListBox.GetItemChecked(6);
                I1 = BuildingOccupancyListBox.GetItemChecked(7);
                I3 = BuildingOccupancyListBox.GetItemChecked(8);
                S1 = BuildingOccupancyListBox.GetItemChecked(9);
                S2 = BuildingOccupancyListBox.GetItemChecked(10);

                //PARAGRAGPH REPLACE PER OCCUPANCY
                if ((A1 == true || A2 == true || A3 == true) && R1 == true && R2 == false)
                {
                    this.FindAndReplace(wordApp, "FireAR1", "For Assembly occupancies, FFPC, NFPA 101 Section 12.3.2 and Hotel occupancies, FFPC, NFPA 101 Section 28.3.2 states that rooms containing high-pressure boilers, large transformers, or other service equipment subject to explosion shall not be located directly under or abutting required exits." +
                        "Hotel units must be separated from adjacent hotel units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 28.3.7.  The hotel unit separation in FBC Section 708 is 1-hour fire partition.");
                }
                else if ((A1 == false && A2 == false && A3 == false) && R1 == true && R2 == false)
                {
                    this.FindAndReplace(wordApp, "FireAR1", "For Hotel occupancies, FFPC, NFPA 101 Section 28.3.2 states that rooms containing high-pressure boilers, large transformers, or other service equipment subject to explosion shall not be located directly under or abutting required exits." +
                        "Hotel units must be separated from adjacent hotel units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 28.3.7.  The hotel unit separation in FBC Section 708 is 1-hour fire partition.");
                }
                else if ((A1 == true || A2 == true || A3 == true) && R1 == false && R2 == false)
                {
                    this.FindAndReplace(wordApp, "FireAR1", "For Assembly occupancies, FFPC, NFPA 101 Section 12.3.2 states that rooms containing high-pressure boilers, large transformers, or other service equipment subject to explosion shall not be located directly under or abutting required exits.");
                }
                else if ((A1 == false && A2 == false && A3 == false) && R1 == false && R2 == true)
                {
                    this.FindAndReplace(wordApp, "FireAR1", "Dwelling units must be separated from adjacent dwelling units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 30.3.7.  The dwelling unit separation in Section FBC Section 708 is 1-hour fire partition.");
                }
                else if((A1 == true || A2 == true || A3 == true) && R1 == false && R2 == true)
                {
                    this.FindAndReplace(wordApp, "FireAR1", "For Assembly occupancies, FFPC, NFPA 101 Section 12.3.2 states that rooms containing high-pressure boilers, large transformers, or other service equipment subject to explosion shall not be located directly under or abutting required exits."
                        + "Dwelling units must be separated from adjacent dwelling units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 30.3.7.  The dwelling unit separation in Section FBC Section 708 is 1-hour fire partition.");
                }
                else if((A1 == true && A2 == true && A3 == true) && R1 == true && R2 == true)
                {
                    this.FindAndReplace(wordApp, "FireAR1", "For Assembly occupancies, FFPC, NFPA 101 Section 12.3.2 and Hotel occupancies, FFPC, NFPA 101 Section 28.3.2 states that rooms containing high-pressure boilers, large transformers, or other service equipment subject to explosion shall not be located directly under or abutting required exits." +
                        "Hotel units must be separated from adjacent hotel units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 28.3.7.  The hotel unit separation in FBC Section 708 is 1-hour fire partition." + "Dwelling units must be separated from adjacent dwelling units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 30.3.7.  The dwelling unit separation in Section FBC Section 708 is 1-hour fire partition.");
                }
                else if((A1 == false && A2 == false && A3 == false) && R1 == true && R2 == true)
                {
                    this.FindAndReplace(wordApp, "FireAR1", "For Hotel occupancies, FFPC, NFPA 101 Section 28.3.2 states that rooms containing high-pressure boilers, large transformers, or other service equipment subject to explosion shall not be located directly under or abutting required exits." +
                        "Hotel units must be separated from adjacent hotel units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 28.3.7.  The hotel unit separation in FBC Section 708 is 1-hour fire partition." + "Dwelling units must be separated from adjacent dwelling units by ½-hr fire barriers in accordance with FFPC, NFPA 101 Section 30.3.7.  The dwelling unit separation in Section FBC Section 708 is 1-hour fire partition.");
                }
                else
                {
                    this.FindAndReplace(wordApp, "FireAR1", "DELETE");
                }

                if (I1 == true || I3 == true)
                {
                    this.FindAndReplace(wordApp, "I1I3PARTITION", "Every story shall be divided into not less than two smoke compartments (FBC §420.4, FFPC, NFPA 101 FBC §32.3.3.7).  Each smoke compartment shall have an area not exceeding 22,500 square feet and the maximum travel distance from any point to reach a door in the smoke barrier shall not exceed 200 feet. Smoke barriers shall be constructed in accordance with FFPC, NFPA 101 §8.5 and shall have a minimum 1 - hour fire resistance rating(FFPC, NFPA 101 §32.3.3.7.8).Smoke barrier doors shall be at least 1 ¼ in.thick, solid - bonded wood - core doors, or shall be fire rated for at least 20 minutes(FFPC, NFPA 101 §32.3.3.7.13).At least 15 net square feet per resident shall be provided within the aggregate area of corridors, lounge or dining areas, and other low hazard areas on each side of the smoke barrier(FBC §420.4.1, FFPC, NFPA 101 §32.3.3.7.11), and not less than 6 net square feet for other occupants.");
                }
                else
                {
                    this.FindAndReplace(wordApp, "I1I3PARTITION", "DELETE");
                }

                // Table 1 Editing per Occupancy Input
                Microsoft.Office.Interop.Word.Table table1 = myWordDoc.Tables[1];
                if (S2 == false)
                {
                    table1.Rows[11].Delete();
                }
                if (S1 == false)
                {
                    table1.Rows[10].Delete();
                }
                if (I3 == false)
                {
                    table1.Rows[9].Delete();
                }
                if (I1 == false)
                {
                    table1.Rows[8].Delete();
                }
                if (R2 == false)
                {
                    table1.Rows[7].Delete();
                }
                if (R1 == false)
                {
                    table1.Rows[6].Delete();
                }
                if (M == false)
                {
                    table1.Rows[5].Delete();
                }
                if (B == false)
                {
                    table1.Rows[4].Delete();
                }
                if (A3 == false)
                {
                    table1.Rows[3].Delete();
                }
                if (A2 == false)
                {
                    table1.Rows[2].Delete();
                }
                if (A1 == false)
                {
                    table1.Rows[1].Delete();
                }

                //-----------------------------------------------------------------How do these work?--------------------------------------------------------------------
                //Table 8 Fire rating of spaces
                Microsoft.Office.Interop.Word.Table table8 = myWordDoc.Tables[8];
                if (R1 == false && R2 == false)
                {
                    table8.Rows[11].Delete();
                    table8.Rows[8].Delete();
                    
                }

                // Table 10 Editing per Occupancy Input
                Microsoft.Office.Interop.Word.Table table10 = myWordDoc.Tables[10];

                if (S1 == false || S2 == false)
                {
                    table10.Rows[8].Delete();
                }
                if (M == false)
                {
                    table10.Rows[7].Delete();
                }
                if (R1 == false)
                {
                    table10.Rows[6].Delete();
                }
                if (R2 == false)
                {
                    table10.Rows[5].Delete();
                }

                if (I3 == false)
                {
                    table10.Rows[4].Delete();
                }
                if (I1 == false)
                {
                    table10.Rows[3].Delete();
                }
                if (B == false)
                {
                    table10.Rows[2].Delete();
                }
                if (A1 == false && A2 == false && A3 == false)
                {
                    table10.Rows[1].Delete();
                }


                //Table 11 editing
                Microsoft.Office.Interop.Word.Table table11 = myWordDoc.Tables[11];
                if (S2 == false)
                {
                    table11.Rows[9].Delete();
                }
                if (S1 == false)
                {
                    table11.Rows[8].Delete();
                }
                if (R2 == false)
                {
                    table11.Rows[7].Delete();
                }

                if (R1 == false)
                {
                    table11.Rows[6].Delete();
                }
                if (M == false)
                {
                    table11.Rows[5].Delete();
                }
                if (I3 == false)
                {
                    table11.Rows[4].Delete();
                }
                if (I1 == false)
                {
                    table11.Rows[3].Delete();
                }
                if (B == false)
                {
                    table11.Rows[2].Delete();
                }
                if (A1 == false && A2 == false && A3 == false)
                {
                    table11.Rows[1].Delete();
                }
                /*
                //TABLE 13 EDITING ------------------------------------------------------------------------------------------------------
                Microsoft.Office.Interop.Word.Table table13 = myWordDoc.Tables[13];
                if (A1 == false && A2 == false && A3 == false)
                {
                    table13.Rows[20].Delete();
                    table13.Rows[18].Delete();
                    table13.Rows[16].Delete();
                    table13.Rows[10 - 12].Delete();
                    table13.Rows[1 - 6].Delete();
                }
                if (B == false)
                {
                    table13.Rows[15].Delete();
                }

                if (R1 == false && R2 == false)
                {
                    table13.Rows[19].Delete();
                }
                if (I1 == false && I3 == false)
                {
                    table13.Rows[5].Delete();
                    table13.Rows[7 - 10].Delete();
                }
                if (S2 == false)
                {
                    table13.Rows[17].Delete();
                }
                if (M == false)
                {
                    table13.Rows[13].Delete();
                }
                */

                //TABLE 14 EDITING
                Microsoft.Office.Interop.Word.Table table14 = myWordDoc.Tables[14];

                if (I3 == false)
                {
                    table14.Rows[8].Delete();
                }
                if (I1 == false)
                {
                    table14.Rows[7].Delete(); ;
                }
                if (R1 == false)
                {
                    table14.Rows[6].Delete();
                }
                if (R2 == false)
                {
                    table14.Rows[5].Delete();
                }
                if (M == false)
                {
                    table14.Rows[3].Delete();
                }
                if (B == false)
                {
                    table14.Rows[2].Delete();
                }
                if (A1 == false && A2 == false && A3 == false)
                {
                    table14.Rows[1].Delete();
                }

                //TABLE 15 AND 16 EDITING

                Microsoft.Office.Interop.Word.Table table15 = myWordDoc.Tables[15];
                Microsoft.Office.Interop.Word.Table table16 = myWordDoc.Tables[16];
                if (isEmergencyVoiceSystem.Checked && intBuildingHeight >= 75 && isSprinklered.Checked)
                {

                    //Delete Table 15 ------------------------------
                    table15.Delete();
                    if (I1 == true || I3 == true)
                    {
                        table16.Rows[4].Delete();
                        table16.Rows[2].Delete();
                        //what do we do for healthcare table16.Rows[3].Delete();
                    
                    }
                    else
                    {
                        table16.Rows[3].Delete();
                        table16.Rows[2].Delete();
                        table16.Rows[1].Delete();
                    }
                }
                else
                {
                    //Delete Table 16 -------------------------------
                    table16.Delete();
                    if (I1 == true || I3 == true)
                    {
                        table15.Rows[4].Delete();
                        table15.Rows[2].Delete();
                        //what do we do for healthcare table15.Rows[3].Delete();

                    }
                    else
                    {
                        table15.Rows[3].Delete();
                        table15.Rows[2].Delete();
                        table15.Rows[1].Delete();
                    }
                }

                //TABLE 17 FIRE EXTINGUISHERS
                Microsoft.Office.Interop.Word.Table table17 = myWordDoc.Tables[17];
                if(R1 == true || R2 == true || A1 == true || A2 == true || A3 == true || B == true)
                {
                    table17.Rows[3].Delete();
                    //if (COMKITCHEN == false)
                    //{
                     //{
                     //    table17.Rows[1].Delete();
                     //}
                    //}
                }
                else if(S1 == true || S2 == true)
                {
                    table17.Rows[2].Delete();
                    //if (COMKITCHEN == false)
                    //{
                    //    table17.Rows[1].Delete();
                    //}
                }
                //ELEVATOR SECTION

                //STAIR SECTION

                //FIRE ALARM SECTION ----------------------------------------------------------------------------------
                //if(R1 == true)
                //{
                //
                //}

                //MEANS OF ESCAPE SECTION
                if ((R1 == true || R2 == true) && isSprinklered.Checked) //required for R2 with only one exit =========================
                {
                    this.FindAndReplace(wordApp, "MeansEscape", "Secondary means of escape windows are not required in dwelling units  [hotel units] when the building is protected by an automatic sprinkler system per FFPC, NFPA 101.  Emergency escape/rescue windows are required by FBC Section 1030 for only for R-2 occupancies in buildings that have only one exit.  The rescue windows are required even if the building is protected by an automatic sprinkler system.");

                    //SMOKE ALARM SECTION
                    this.FindAndReplace(wordApp, "R1R2SMOKEALARM", "FBC Section 907.2.11 and FFPC, NFPA 101 Section 28.3.4.5 and Section 30.3.4.5 require that single- or multiple-station smoke alarms be installed and maintained in Residential Apartment (R-2) and Hotel Occupancy (R-1) in all of the following locations:");
                    this.FindAndReplace(wordApp, "R1R2SMOKEALARM1", "On the ceiling or wall - located outside of each separate sleeping area in the immediate vicinity of bedrooms. ");
                    this.FindAndReplace(wordApp, "R1R2SMOKEALARM2", "In each room used for sleeping purposes. ");
                    this.FindAndReplace(wordApp, "R1R2SMOKEALARM3", "In each story within a dwelling unit. ");
                    this.FindAndReplace(wordApp, "R1R2SMOKEALARMEND", "Where more than one (1) smoke alarm is required to be installed within an individual dwelling unit the smoke alarms should be interconnected in a manner such that the activation of one (1) alarm will activate all of the alarms within the individual unit. The alarm should be clearly audible in all bedrooms over background noise levels with all intervening doors closed.");

                    //EMERGENCY POWER SECTION
                    this.FindAndReplace(wordApp, "R1R2EMERGENCYPOWER", "An 8-hour fuel supply shall be provided on life safety equipment within the building. It is noted that it is common practice to include the following systems: domestic water pumps; jockey pumps; telephone and security systems.");
                    if(BLDGBOX.SelectedItem.ToString() == "City of Miami" || FIREBOX.SelectedItem.ToString() == "City of Miami")
                    {
                        this.FindAndReplace(wordApp, "R1R2EMERGENCYPOWERCOM", "In the City of Miami, the fire department will require a 24-hour fuel supply for emergency power.");
                    }

                }

                //SITE ACCESS - IF MIAMI DADE
                if(FIREBOX.SelectedItem.ToString() == "Miami Dade")
                {
                    this.FindAndReplace(wordApp, "MiamiDadeSite", "Miami-Dade Fire Department requires set-up sites located at a minimum on two sides of the building at the approximate center of each side for firefighting and rescue operations. Depending upon the building configuration, additional set-up sites may be required by the AHJ.  Sites shall be no closer than 10 feet and no further than 30 feet from any building.  Each site shall be a minimum of 21 feet wide and 47 feet long with a cross slope no greater than 5 percent. Sites shall comply with the requirements of the emergency vehicle support capabilities above and also capable of withstanding any point forces resulting from outriggers.  Set-up sites, fire lanes, and slopes in a project must be able to accommodate a truck with dimensions as follows: 47 feet overall length, 36 feet bumper to bump, and 256 inches wheelbase length.");
                }


                //SMOKE DETECTION SECTION -----------------------------------------


                //SMOKE REMOVAL SECTION
                if(intBuildingHeight >= 75)
                {
                    this.FindAndReplace(wordApp, "SMOKEREMOVAL", "A smoke removal system is required for high-rise buildings as indicated in FBC Section 403.4.7.  Natural or mechanical ventilation must be provided for this project to facilitate smoke removal in post-fire salvage and overhaul operations in accordance with Section 403.4.7. Natural ventilation involves manually operable windows or panels distributed around the perimeter of each floor based on the criteria in FBC Section 403.4.7(1).  If natural means is not feasible, then mechanical ventilation must be provided to achieve one exhaust air change every 15 minutes for the area involved.  Return and exhaust air shall be moved directly to the outside without recirculation to other portions of the building.");
                    this.FindAndReplace(wordApp, "SCRADESIGN", "The Smoke Control Rational Analysis for this project will provide more detailed design criteria for the required smoke control systems.");
                }


                //LUMINOUS EGRESS MARKINGS - NEED TO ADD CODE FOR R2 BUILDINGS WITH NON R2 ACCESSORY SPACES ABOVE THE 75FT
                if (R2 == false && intBuildingHeight >= 75)
                {
                    this.FindAndReplace(wordApp, "LUMINOUSMARKSECTION", "As a high-rise building, FBC §403.5.5 states that approved luminous egress path markings delineating the exit path must be provided in Group A, B, E, I, M and R-1 occupancies in accordance with FBC §1025.  Markings within the exit enclosures are required to be provided on steps, landings, handrails, perimeter demarcation lines, and discharge doors from the exit enclosure.  Materials should comply with either UL 1994 or ASTM E2072. ");
                }

                //EXIT ACCESS SECTION
                if (R1 == true || I1 == true)
                {
                    this.FindAndReplace(wordApp, "R1I1UnitExit", "For Hotel Group R-1 occupancies and Res B/C, the FFPC requires two exit access doors from the unit when the guest room or guest suite is over 2,000 sq.ft.  The exit access doors must be located remotely from each other (FFPC, NFPA 101 Section 28.2.5.7).  If limits shown in Table 12 are exceeded, then additional exits must be provided.");
                }

                //OCCUPANT EVAC OR ADDITIONAL STAIR
                if (intBuildingHeight >= 420 && R2 == false)
                {
                    this.FindAndReplace(wordApp, "OEESection", "For buildings greater than 420 ft. in building height, other than R-2 buildings, one additional stairway must be provided in addition to above exit stairs per FBC Section 403.5.2.  There is an alternate provision to the additional stair, which states that an occupant evacuation elevator can be provided in lieu of the stair.  The occupant evacuation elevator, separate from fire service access elevator, must comply with FBC Section 3008 and FFPC, NFPA 101 Section 7.14." + "NOTE: Where the spaces above 420 ft that are not R-2 are accessory to the R-2 building, an additional stairwary or Occupant Evacuation Elevator is not required.");
                }

                //LOOPED CORRIDOR FOR R1/R2
                if ((R2 == true || R1 == true) && isNONLooped.Checked)
                {
                    this.FindAndReplace(wordApp, "NONLoopedcorridor", "For R - 2 and R - 1 occupancies, the distance between exits is not applicable to common nonlooped exit access corridors in a building that has corridor doors from the guestroom or guest suite or dwelling unit, which are arranged so that the exits are located in opposite directions from such doors (FBC Section 1007.1.1 Exception 3).The exit discharge must also meet the remoteness requirement.");
                }

                //STREET FLOOR REQ
                if (B == true || R1 == true || I1 == true)
                {
                    this.FindAndReplace(wordApp, "StreetFloorREQ", "For Business (FFPC, NFPA 101 Section 38.2.3.3), Hotel (FFPC, NFPA 101 Section 28.2.3.2) and Res B/C,(check others), the code requires that street floor exits must accommodate the occupant load of street floor plus stair discharging onto street floor.");
                }

                //DOOR LOCK I1
                if (I1 == true)
                {
                    this.FindAndReplace(wordApp, "DoorLockI1", "Door locking arrangements shall be permitted where clinical needs of residents require specialized security measures or where residents pose a security threat provided the staff can always readily unlock doors and the building is protected with an approved automatic sprinkler system (FFPC §32.3.2.2.2(6)). Doors in the means of egress permitted to be locked must have provisions for the rapid removal of occupants by means of remote-control locks from within the locked building, keying of all locks to keys always carried by staff, or other reliable means. Only one locking device shall always be permitted (FFPC §32.3.2.2.2 (7)(8)).");
                }
                else
                {
                    this.FindAndReplace(wordApp, "DoorLockI1", "DELETE");
                }

                //MEZZ SECTION
                if (isMezz.Checked)
                {
                    this.FindAndReplace(wordApp, "MEZZSECTION", "Mezzanines within the PNAME  project are designed to comply with the requirements of the FBC and the FFPC. Currently there is a mezzanine located on ______. A mezzanine or mezzanines must comply with FBC Section 505.2.  and shall be considered a portion of the story below. Such mezzanines shall not contribute to either the building area or number of stories as regulated by Section 503.1. The area of the mezzanine shall be included in determining the fire area. The clear height above and below the mezzanine floor construction shall be not less than 7 feet. The aggregate area of a mezzanine or mezzanines within a room shall be not greater than one - third of the floor area of that room or space in which they are located.The enclosed portion of a room shall not be included in a determination of the floor area of the room in which the mezzanine is located.In determining the allowable mezzanine area, the area of the mezzanine shall not be included in the floor area of the room. (FBC Section 505.2.1). The floor area of a mezzanine, or the aggregate floor area of multiple mezzanines, shall not exceed one - half of the floor area of the room or story in which the mezzanines are located; otherwise, such mezzanine or aggregated mezzanines shall be treated as floors (FFPC, NFPA 101 Section 37.1.2.2.3). The means of egress for mezzanines shall comply with FBC Section 505.2.2 and FFPC, NFPA 101 Section 12.2.4.5.");
                }

                //STAGE SECTION
                if (isStage.Checked)
                {
                    this.FindAndReplace(wordApp, "STAGESECTION", "Currently, there is a stage/platform area located within the project. Stages and platforms must comply with FBC Section 410.3 and 410.4 and FFPC, NFPA 101 Section 12.4.6. It is assumed the stage area will be less than 1,000 square feet. Where the stage height is greater than 50 feet, all portions of the stage shall be separated from the seating area by a proscenium wall with not less than a 2 - hour fire - resistance rating extending continuously from the foundation to the roof(FBC 410.3.4). Emergency ventilation shall be provided for stages larger than 1, 000 square feet in floor area, or with a stage height greater than 50 feet.Such ventilation shall comply with Section 410.3.7.1 or 410.3.7.2(FBC 410.3.7). Permanent platforms shall be constructed of materials as required for the type of construction of the building in which the permanent platform is located.Permanent platforms are permitted to be constructed of fire - retardant - treated wood for Types I and II construction where the platforms are not more than 30 inches above the main floor, and not more than one - third of the room floor area, and not more than 3, 000 square feet in area.Where the space beneath the permanent platform is used for storage or any purpose other than equipment, wiring or plumbing, the floor assembly shall be not less than 1 - hour fire - resistance - rated construction.Where the space beneath the permanent platform is used only for equipment, wiring or plumbing, the underside of the permanent platform need not be protected (FBC Section 410.4). Combustible materials used in sets and scenery shall meet the fire propagation performance criteria of Test Method 1 or Test Method 2, as appropriate, of NFPA 701, in accordance with FFPC Section 806. Foam plastics and materials containing foam plastics shall comply with FBC Section 2603 (FBC Section 410.3.6). The stage shall be separated from dressing rooms, scene docks, property rooms, workshops, storerooms, and compartments appurtenant to the stage and other parts of the building by fire barriers constructed in accordance with FBC Section 707 or Section 711, or both.The fire-resistance rating shall be not less than 1 hour for stage heights of 50 feet or less(FBC Section 410.5.1). For technical production areas, the exit access travel distance shall be not greater than 400 feet for buildings equipped throughout with an automatic sprinkler system.Where two means of egress are required, the common path of travel shall be not greater than 100 feet (FBC Section 410.6.3) and the egress width shall be not less than 22 inches(FBC Section 410.6.3.5). Exit access stairways and ramps serving a stage or platform are not required to be enclosed.Exit access stairways and ramps serving technical production areas are not required to be enclosed (FBC Section 410.6.2). Stages shall be equipped with an automatic sprinkler system in accordance with FBC Section 903.3.1.1. Sprinklers shall be installed under the roof and gridiron and under all catwalks and galleries over the stage.Sprinklers shall be installed in dressing rooms, performer lounges, shops, and storerooms accessory to such stages. Sprinklers are not required under stage areas less than 4 feet in clear height that are utilized exclusively for storage of tables and chairs, provided the concealed space is separated from the adjacent spaces by Type X gypsum board not less than 5/8-inch in thickness. Sprinklers are not required for stages 1,000 square feet or less in area and 50 feet or less in height where curtains, scenery or other combustible hangings are not retractable vertically.Combustible hangings shall be limited to a single main curtain, borders, legs, and a single backdrop (FBC Section 410.7).");
                }

                //SPRINKLER SECTION
                if(intBuildingHeight >= 420)
                {
                    this.FindAndReplace(wordApp, "SPRINKLER420", "The building is greater than 420 ft and so will be supplied by a minimum of two risers.  Each riser shall supply sprinkler on alternate floors.  If more  than two risers are provided for a zone, then sprinklers on adjacent floors shall not be supplied from the same riser (FBC Section 403.3).");
                    //SITE WATER SECTION
                    this.FindAndReplace(wordApp, "MAINWATER420", "The building is greater than 420 ft, therefore, the water supply must be designed so that there are separate connections to a minimum of two public mains on different streets (FBC Section 403.3).");
                }
                if(BLDGBOX.SelectedItem.ToString() == "Miami Dade")
                {
                    this.FindAndReplace(wordApp, "SPRINKLERTERRACE", "Miami Dade Fire Department will require sprinkler protection in all balconies with a depth of four (4) feet.  The fire department has accepted alternative solutions if sprinkler head installation in the balcony is not feasible.  Further discussion on this item required.");
                }
                else
                {
                    this.FindAndReplace(wordApp, "SPRINKLERTERRACE", "The fire department will require sprinkler protection in terraces that are four (4) feet or more in depth.  A terrace is usually recessed within footprint of the building and has living space on three walls.  Open balconies (cantilevered) will not require sprinkler protection even if there is a privacy partition between balconies.");
                }
                //STAIR DISCHARGE SECTION ----- ADD PICTURE

                //FCC SECTION
                if(BLDGBOX.SelectedItem.ToString() == "City of Miami" || BLDGBOX.SelectedItem.ToString() == "Miami Dade")
                {
                    this.FindAndReplace(wordApp, "COMandMDFCC", "Miami Dade County and City of Miami Fire Department requires a door opening into the lobby and additional door opening to the outside to provide direct access without entering the lobby.   The fire command center shall be located on the address side/main entrance of the building and shall be within proximity to the fire service access elevators and stairs that have a standpipe available for fire operations.");
                }
                //add FCC PICTURE --------------------------------------------------------------

                //MONITORING SECTION
                if(intBuildingHeight >= 120)
                {
                    this.FindAndReplace(wordApp, "FSESprinkler", "For high-rise buildings with a fire service elevator, the sprinkler system shall have a sprinkler control valve supervisory switch and water-flow-initiating device provided for each floor that is monitored by the buildings fire alarm system in accordance with FBC Section 3007.2.2.");
                    //STANDPIPE SECTION
                    this.FindAndReplace(wordApp, "STANDPIPE120", "The standpipe located in an exit enclosure shall have access to the floor without passing through the fire service elevator lobby (FBC Section 3007.9.1). However, in a high-rise R-2 or R-1 occupancy building, standpipes must be located in stairwells and are subject to only the requirements of the FFPC and NFPA 14, adopted by the State Fire Marshal.");
                }

                //FIRE PUMP 
                //ADD CODE FOR HIGHZONE PUMP ----------------------
                //INSERT FIRE PUMP ROOM PICTURE----------------------------------------

                //SUPPRESSION SYSTEM SECTION
                //ADD COMMERCIAL KITCHEN AND WHEN SUPPRESSION SYSTEM NEEDED (SUPPRESSIONSYSTEM)


                //PANIC HARDWARE A OR E ----- Need to add E to Program

                if (A1 == true || A2 == true || A3 == true) //Or E is true)
                {
                    this.FindAndReplace(wordApp, "PanicHardwareREQ", "Panic hardware (or fire exit hardware for fire doors) must be installed in all doors serving rooms or spaces with an occupant load of 50 persons or more in a Group A or E occupancy per FBC Section 1010.1.10.  The FFPC, Section 12.2.2.2.3, has a similar requirement for assembly occupancies where the occupancy load is 100 or more. Therefore, the FBC has the more stringent requirement and must be implemented.  Panic hardware must be installed in electrical rooms as stated in other section of this report.");
                }

                //Type Construction
                string buildingTypeA2Floor = null;
                string buildingTypeA3Floor = null;
                string buildingTypeBFloor = null;
                string buildingTypeMFloor = null;
                string buildingTypeR1Floor = null;
                string buildingTypeR2Floor = null;
                string buildingTypeS1Floor = null;
                string buildingTypeS2Floor = null;

                string buildingTypeA2SQF = null;
                string buildingTypeA3SQF = null;
                string buildingTypeBSQF = null;
                string buildingTypeMSQF = null;
                string buildingTypeR1SQF = null;
                string buildingTypeR2SQF = null;
                string buildingTypeS1SQF = null;
                string buildingTypeS2SQF = null;

                if (A2 == true || A3 == true)
                {
                    //---------A2--------------------------//
                    //highest floor
                    if (int.Parse(A2HighestFloor.Text) <= 3)
                    {
                        buildingTypeA2Floor = "Type IIB";
                    }
                    else if (int.Parse(A2HighestFloor.Text) == 4)
                    {
                        buildingTypeA2Floor = "Type IIA";
                    }
                    else if ((int.Parse(A2HighestFloor.Text) >= 5 && int.Parse(A2HighestFloor.Text) <= 12))
                    {
                        buildingTypeA2Floor = "Type IB";
                    }
                    else if (int.Parse(A2HighestFloor.Text) > 12)
                    {
                        buildingTypeA2Floor = "Type IA";
                    }
                    //area value
                    if(int.Parse(A2Area.Text) <= 28500)
                    {
                        buildingTypeA2SQF = "Type IIB";
                    }
                    else if(int.Parse(A2Area.Text) > 28500 && int.Parse(A2Area.Text) <= 46500)
                    {
                        buildingTypeA2SQF = "Type IIA";
                    }
                    else if(int.Parse(A2Area.Text) > 46500)
                    {
                        buildingTypeA2SQF = "Type IB";
                    }

                    //-------------A3--------------------//
                    //highest floor
                    if (int.Parse(A3HighestFloor.Text) <= 3)
                    {
                        buildingTypeA3Floor = "Type IIB";
                    }
                    else if (int.Parse(A3HighestFloor.Text) == 4)
                    {
                        buildingTypeA3Floor = "Type IIA";
                    }
                    else if ((int.Parse(A3HighestFloor.Text) >= 5 && int.Parse(A3HighestFloor.Text) <= 12))
                    {
                        buildingTypeA3Floor = "Type IB";
                    }
                    else if (int.Parse(A3HighestFloor.Text) > 12)
                    {
                        buildingTypeA3Floor = "Type IA";
                    }
                    //Area Value
                    if (int.Parse(A3Area.Text) <= 28500)
                    {
                        buildingTypeA3SQF = "Type IIB";
                    }
                    else if (int.Parse(A3Area.Text) > 28500 && int.Parse(A3Area.Text) <= 46500)
                    {
                        buildingTypeA3SQF = "Type IIA";
                    }
                    else if (int.Parse(A3Area.Text) > 46500)
                    {
                        buildingTypeA3SQF = "Type IB";
                    }


                }

                if (B == true)
                {
                    //----------------B-------------------
                    ////highest floor
                    //What about when Bfloor is below 4?
                    if (int.Parse(BHighestFloor.Text) == 4)
                    {
                        buildingTypeBFloor = "Type IIB";
                    }
                    else if (int.Parse(BHighestFloor.Text) > 4 && int.Parse(BHighestFloor.Text) <= 6)
                    {
                        buildingTypeBFloor = "Type IIA";
                    }
                    else if (int.Parse(BHighestFloor.Text) > 6 && int.Parse(BHighestFloor.Text) <= 12)
                    {
                        buildingTypeBFloor = "Type IB";
                    }
                    else if (int.Parse(BHighestFloor.Text) > 12)
                    {
                        buildingTypeBFloor = "Type IA";
                    }
                    //Area Value
                    if (int.Parse(BArea.Text) <= 69000)
                    {
                        buildingTypeBSQF = "Type IIB";
                    }
                    else if (int.Parse(BArea.Text) > 69000 && int.Parse(BArea.Text) <= 112500)
                    {
                        buildingTypeBSQF = "Type IIA";
                    }
                    else if (int.Parse(BArea.Text) > 112500)
                    {
                        buildingTypeBSQF = "Type IB";
                    }
                }

                if (M == true)
                {
                    //---------------------M---------------------
                    //highest floor
                    //What if it is less than 3?
                    if (int.Parse(MHighestFloor.Text) == 3)
                    {
                        buildingTypeMFloor = "Type IIB";
                    }
                    else if (int.Parse(MHighestFloor.Text) > 3 && int.Parse(MHighestFloor.Text) <= 5)
                    {
                        buildingTypeMFloor = "Type IIA";
                    }
                    else if (int.Parse(MHighestFloor.Text) > 5 && int.Parse(MHighestFloor.Text) <= 12)
                    {
                        buildingTypeMFloor = "Type IB";
                    }
                    else if (int.Parse(MHighestFloor.Text) > 12)
                    {
                        buildingTypeMFloor = "Type IA";
                    }
                    //Area Value
                    if (int.Parse(MArea.Text) <= 37500)
                    {
                        buildingTypeMSQF = "Type IIB";
                    }
                    else if (int.Parse(MArea.Text) > 37500 && int.Parse(MArea.Text) <= 64500)
                    {
                        buildingTypeMSQF = "Type IIA";
                    }
                    else if (int.Parse(MArea.Text) > 64500)
                    {
                        buildingTypeMSQF = "Type IB";
                    }
                }

                if (R1 == true)
                {
                    //--------------------R1--------------
                    //highest floor
                    //What if R1 highest floor is below 5?
                    if (int.Parse(R1HighestFloor.Text) == 5)
                    {
                        buildingTypeR1Floor = "Type IIB";
                    }
                    else if (int.Parse(R1HighestFloor.Text) > 5 && int.Parse(R1HighestFloor.Text) <= 12)
                    {
                        buildingTypeR1Floor = "Type IB";
                    }
                    else if (int.Parse(R1HighestFloor.Text) > 12)
                    {
                        buildingTypeR1Floor = "Type IA";
                    }
                    //Area Value
                    if (int.Parse(R1Area.Text) <= 48000)
                    {
                        buildingTypeR1SQF = "Type IIB";
                    }
                    else if (int.Parse(R1Area.Text) > 48000 && int.Parse(R1Area.Text) <= 72000)
                    {
                        buildingTypeR1SQF = "Type IIA";
                    }
                    else if (int.Parse(R1Area.Text) > 72000)
                    {
                        buildingTypeR1SQF = "Type IB";
                    }
                }

                if (R2 == true)
                {
                    //---------------------R2--------------------
                    //highest floor
                    //What about when R2Highest is below 5?
                    if (int.Parse(R2HighestFloor.Text) == 5)
                    {
                        buildingTypeR2Floor = "Type IIB";
                    }
                    else if (int.Parse(R2HighestFloor.Text) > 5 && int.Parse(R2HighestFloor.Text) <= 12)
                    {
                        buildingTypeR2Floor = "Type IB";
                    }
                    else if (int.Parse(R2HighestFloor.Text) > 12)
                    {
                        buildingTypeR2Floor = "Type IA";
                    }
                    //Area Value
                    if (int.Parse(R2Area.Text) <= 48000)
                    {
                        buildingTypeR2SQF = "Type IIB";
                    }
                    else if (int.Parse(R2Area.Text) > 48000 && int.Parse(R2Area.Text) <= 72000)
                    {
                        buildingTypeR2SQF = "Type IIA";
                    }
                    else if (int.Parse(R2Area.Text) > 72000)
                    {
                        buildingTypeR2SQF = "Type IB";
                    }
                }

                if (S1 == true)
                {
                    //------------------------S1------------------
                    //highest floor
                    //What about when S1Highest is below 3?
                    if (int.Parse(S1HighestFloor.Text) == 3)
                    {
                        buildingTypeS1Floor = "Type IIB";
                    }
                    else if (int.Parse(S1HighestFloor.Text) > 3 && int.Parse(S1HighestFloor.Text) <= 5)
                    {
                        buildingTypeS1Floor = "Type IIA";
                    }
                    else if (int.Parse(S1HighestFloor.Text) > 5 && int.Parse(S1HighestFloor.Text) <= 12)
                    {
                        buildingTypeS1Floor = "Type IB";
                    }
                    else if (int.Parse(S1HighestFloor.Text) > 12)
                    {
                        buildingTypeS1Floor = "Type IA";
                    }
                    //Area Value
                    if (int.Parse(S1Area.Text) <= 52500)
                    {
                        buildingTypeS1SQF = "Type IIB";
                    }
                    else if (int.Parse(S1Area.Text) > 52500 && int.Parse(S1Area.Text) <= 78000)
                    {
                        buildingTypeS1SQF = "Type IIA";
                    }
                    else if (int.Parse(S1Area.Text) > 78000 && int.Parse(S1Area.Text) <= 144000)
                    {
                        buildingTypeS1SQF = "Type IB";
                    }
                    else if (int.Parse(S1Area.Text) > 144000)
                    {
                        buildingTypeS1SQF = "Type IA";
                    }
                }

                if (S2 == true)
                {
                    //---------------------S2------------------
                    //highest floor
                    //What about when S2Highest is below 4?
                    if (int.Parse(S2HighestFloor.Text) == 4)
                    {
                        buildingTypeS2Floor = "Type IIB";
                    }
                    else if (int.Parse(S2HighestFloor.Text) > 4 && int.Parse(S2HighestFloor.Text) <= 6)
                    {
                        buildingTypeS2Floor = "Type IIA";
                    }
                    else if (int.Parse(S2HighestFloor.Text) > 6 && int.Parse(S2HighestFloor.Text) <= 12)
                    {
                        buildingTypeS2Floor = "Type IB";
                    }
                    else if (int.Parse(S2HighestFloor.Text) > 12)
                    {
                        buildingTypeS2Floor = "Type IA";
                    }
                    //Area Value
                    if (int.Parse(S2Area.Text) <= 78000)
                    {
                        buildingTypeS2SQF = "Type IIB";
                    }
                    else if (int.Parse(S2Area.Text) > 78000 && int.Parse(S2Area.Text) <= 117000)
                    {
                        buildingTypeS2SQF = "Type IIA";
                    }
                    else if (int.Parse(S2Area.Text) > 117000 && int.Parse(S2Area.Text) <= 237000)
                    {
                        buildingTypeS2SQF = "Type IB";
                    }
                    else if (int.Parse(S2Area.Text) > 237000)
                    {
                        buildingTypeS2SQF = "Type IA";
                    }
                }

                //get buildingTypeHeight
                if (intBuildingHeight <= 75)
                {
                    //Set, find and replace BUILDTYPE
                    buildingTypeHeight = "Type IIB";

                    /*
                    this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                    //Delete columns that aren't IIB
                    Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                    for (int i = 0; i < 3; i++)
                    {

                        table2.Columns[2].Delete();
                    }

                    //Delete the other table
                    Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                    table2AndAHalf.Delete();
                    */

                }
                else if (intBuildingHeight > 75 && intBuildingHeight <= 85)
                {
                    //Set, find and replace BUILDTYPE
                    buildingTypeHeight = "Type IIA";

                    /*
                    this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                    //Delete columns that aren't IIA
                    Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                    table2.Columns[5].Delete();
                    for (int i = 0; i < 2; i++)
                    {
                        table2.Columns[2].Delete();
                    }

                    //Delete the other table
                    Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                    table2AndAHalf.Delete();
                    */

                }
                else if (intBuildingHeight > 85 && intBuildingHeight <= 180)
                {

                    if (isSprinklered.Checked)
                    {
                        //Set, find and replace BUILDTYPE
                        buildingTypeHeight = "Type IIA";

                        /*
                        this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                        //Delete columns that aren't IIA
                        Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                        table2.Columns[5].Delete();
                        for (int i = 0; i < 2; i++)
                        {
                            table2.Columns[2].Delete();
                        }

                        //Change Primary Column
                        Microsoft.Office.Interop.Word.Cell primColChange = table2.Cell(3, 2);
                        primColChange.Range.Text = "2";

                        //Delete the other table
                        Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                        table2AndAHalf.Delete();
                        */

                    }
                    else
                    {
                        //Set, find and replace BUILDTYPE
                        buildingTypeHeight = "Type IB";

                        /*
                        this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                        //Delete columns that aren't IB
                        Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                        table2.Columns[5].Delete();
                        table2.Columns[4].Delete();
                        table2.Columns[2].Delete();

                        //Delete the other table
                        Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                        table2AndAHalf.Delete();
                        */

                    }

                }
                else if (intBuildingHeight > 180)
                {

                    //if (intBuildingHeight <= 420)
                    //{
                    //Set, find and replace BUILDTYPE
                    //buildingTypeHeight = "Type IA Reduced";
                    /*
                    this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                    //Delete columns that aren't IB
                    Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                    table2.Columns[5].Delete();
                    table2.Columns[4].Delete();
                    table2.Columns[2].Delete();

                    //Change Primary Column
                    Microsoft.Office.Interop.Word.Cell primColChange = table2.Cell(3, 2);
                    primColChange.Range.Text = "3";

                    //Change Building Element Header
                    Microsoft.Office.Interop.Word.Cell buildElemChange = table2.Cell(1, 2);
                    buildElemChange.Range.Text = "Type IA Reduced";

                    //Delete the other table
                    Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                    table2AndAHalf.Delete();
                    */

                    //}
                    buildingTypeHeight = "Type IA";             
                }

                //Initiate buildingType variable
                string buildingType = "buildType";

                //get actual buildingType
                if(buildingTypeHeight == "IA" || buildingTypeA2Floor == "IA" || buildingTypeA3Floor == "IA" || buildingTypeBFloor == "IA" || buildingTypeMFloor == "IA" ||
                    buildingTypeR1Floor == "IA" || buildingTypeR2Floor == "IA" || buildingTypeS1Floor == "IA" || buildingTypeS2Floor == "IA" || buildingTypeA2SQF == "IA" || 
                    buildingTypeA3SQF == "IA" || buildingTypeBSQF == "IA" || buildingTypeMSQF == "IA" || buildingTypeR1SQF == "IA" || buildingTypeR2SQF == "IA" || buildingTypeS1SQF == "IA"
                    || buildingTypeS2SQF == "IA")
                {
                    buildingType = "IA";

                    if(intBuildingHeight <= 420)
                    {
                        buildingType = "Type IA Reduced";

                        this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                        //Delete columns that aren't IB
                        Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                        table2.Columns[5].Delete();
                        table2.Columns[4].Delete();
                        table2.Columns[2].Delete();
                        //Delete the other table
                        //Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                        //table2AndAHalf.Delete();

                    }
                    else
                    {
                        this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                        //Delete columns that aren't IA
                        Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                        table2.Columns[5].Delete();
                        table2.Columns[4].Delete();
                        table2.Columns[3].Delete();
                        //Delete the other table
                        //Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                        //table2AndAHalf.Delete();

                    }
                }
                else if (buildingTypeHeight == "IB" || buildingTypeA2Floor == "IB" || buildingTypeA3Floor == "IB" || buildingTypeBFloor == "IB" || buildingTypeMFloor == "IB" ||
                    buildingTypeR1Floor == "IB" || buildingTypeR2Floor == "IB" || buildingTypeS1Floor == "IB" || buildingTypeS2Floor == "IB" || buildingTypeA2SQF == "IB" ||
                    buildingTypeA3SQF == "IB" || buildingTypeBSQF == "IB" || buildingTypeMSQF == "IB" || buildingTypeR1SQF == "IB" || buildingTypeR2SQF == "IB" || buildingTypeS1SQF == "IB"
                    || buildingTypeS2SQF == "IB")
                {
                    buildingType = "IB";

                    this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                    //Delete columns that aren't IB
                    Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                    table2.Columns[5].Delete();
                    table2.Columns[4].Delete();
                    table2.Columns[2].Delete();

                    //Delete the other table
                    //Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                    //table2AndAHalf.Delete();
                }
                else if (buildingTypeHeight == "IIA" || buildingTypeA2Floor == "IIA" || buildingTypeA3Floor == "IIA" || buildingTypeBFloor == "IIA" || buildingTypeMFloor == "IIA" ||
                    buildingTypeR1Floor == "IIA" || buildingTypeR2Floor == "IIA" || buildingTypeS1Floor == "IIA" || buildingTypeS2Floor == "IIA" || buildingTypeA2SQF == "IIA" ||
                    buildingTypeA3SQF == "IIA" || buildingTypeBSQF == "IIA" || buildingTypeMSQF == "IIA" || buildingTypeR1SQF == "IIA" || buildingTypeR2SQF == "IIA" || buildingTypeS1SQF == "IIA"
                    || buildingTypeS2SQF == "IIA")
                {
                    buildingType = "IIA";

                    this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                    //Delete columns that aren't IIA
                    Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                    table2.Columns[5].Delete();
                    for (int i = 0; i < 2; i++)
                    {
                        table2.Columns[5].Delete();
                        table2.Columns[3].Delete();
                        table2.Columns[2].Delete();
                    }

                    //Delete the other table
                    //Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                    //table2AndAHalf.Delete();
                }
                else if (buildingTypeHeight == "IIB" || buildingTypeA2Floor == "IIB" || buildingTypeA3Floor == "IIB" || buildingTypeBFloor == "IIB" || buildingTypeMFloor == "IIB" ||
                    buildingTypeR1Floor == "IIB" || buildingTypeR2Floor == "IIB" || buildingTypeS1Floor == "IIB" || buildingTypeS2Floor == "IIB" || buildingTypeA2SQF == "IIB" ||
                    buildingTypeA3SQF == "IIB" || buildingTypeBSQF == "IIB" || buildingTypeMSQF == "IIB" || buildingTypeR1SQF == "IIB" || buildingTypeR2SQF == "IIB" || buildingTypeS1SQF == "IIB"
                    || buildingTypeS2SQF == "IIB")
                {
                    buildingType = "IIB";

                    this.FindAndReplace(wordApp, "BUILDTYPE", buildingType);

                    //Delete columns that aren't IIB
                    Microsoft.Office.Interop.Word.Table table2 = myWordDoc.Tables[2];
                    for (int i = 0; i < 3; i++)
                    {
                        table2.Columns[4].Delete();
                        table2.Columns[3].Delete();
                        table2.Columns[2].Delete();
                    }

                    //Delete the other table
                    //Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                    //table2AndAHalf.Delete();
                }


                //Fire Separation Distance North
                FSDFindAndReplace("NFSD", "NFSDRating", "NFSDOpening", int.Parse(NFSDInput.Text), wordApp, NFSDOccupancy.GetItemText(NFSDOccupancy.SelectedItem), buildingType);
                //Fire Separation Distance South
                FSDFindAndReplace("SFSD", "SFSDRating", "SFSDOpening", int.Parse(SFSDInput.Text), wordApp, SFSDOccupancy.GetItemText(SFSDOccupancy.SelectedItem), buildingType);
                //Fire Separation Distance East
                FSDFindAndReplace("EFSD", "EFSDRating", "EFSDOpening", int.Parse(EFSDInput.Text), wordApp, EFSDOccupancy.GetItemText(EFSDOccupancy.SelectedItem), buildingType);
                //Fire Separation Distance West
                FSDFindAndReplace("WFSD", "WFSDRating", "WFSDOpening", int.Parse(WFSDInput.Text), wordApp, WFSDOccupancy.GetItemText(WFSDOccupancy.SelectedItem), buildingType);

                //Insert uploaded pictures
                FindTextAndReplaceImage(wordApp, myWordDoc, "NEWRPIC", NFSDImage.ImageLocation);
                FindTextAndReplaceImage(wordApp, myWordDoc, "SEWRPIC", SFSDImage.ImageLocation);
                FindTextAndReplaceImage(wordApp, myWordDoc, "EEWRPIC", EFSDImage.ImageLocation);
                FindTextAndReplaceImage(wordApp, myWordDoc, "WEWRPIC", WFSDImage.ImageLocation);


                //Start vertical opening section of narrative
                if (isVO.Checked)
                {
                    if (isMezzanine.Checked)
                    {
                        this.FindAndReplace(wordApp, "MEZZSECTION", "Mezzanines within the PNAME  project are designed to comply with the requirements of the FBC and the FFPC. Currently there is a mezzanine located on ______. A mezzanine or mezzanines must comply with FBC Section 505.2.  and shall be considered a portion of the story below. Such mezzanines shall not contribute to either the building area or number of stories as regulated by Section 503.1. The area of the mezzanine shall be included in determining the fire area. The clear height above and below the mezzanine floor construction shall be not less than 7 feet.The aggregate area of a mezzanine or mezzanines within a room shall be not greater than one - third of the floor area of that room or space in which they are located.The enclosed portion of a room shall not be included in a determination of the floor area of the room in which the mezzanine is located. In determining the allowable mezzanine area, the area of the mezzanine shall not be included in the floor area of the room. (FBC Section 505.2.1). The floor area of a mezzanine, or the aggregate floor area of multiple mezzanines, shall not exceed one - half of the floor area of the room or story in which the mezzanines are located; otherwise, such mezzanine or aggregated mezzanines shall be treated as floors (FFPC, NFPA 101 Section 37.1.2.2.3). The means of egress for mezzanines shall comply with FBC Section 505.2.2 and FFPC, NFPA 101 Section 12.2.4.5.");
                    }
                    else if (isEscalator.Checked)
                    {
                        this.FindAndReplace(wordApp, "ESCALATORSECTION", "These openings must be protected in accordance with FBC Section 712 and FFPC, NFPA 101 Section 8.6.9.7. Per FBC Section 712.1.3.1, the escalators shall be protected by a draft curtain and closely spaced sprinklers in accordance with NFPA 13 where the area of the vertical opening between stories does not exceed twice the horizontal projected area of the escalator.The sprinkler protection around the vertical opening must comply with NFPA 13  Section 8.15.4.  Draft stops (18-inch deep) shall be located immediately adjacent to the opening made of noncombustible material that will stay in place before and during sprinkler operation.In addition, sprinkler heads are spaced 6 feet apart and placed 6-12 inches from drafts top on side away from opening. Another option is to protect the vertical opening by approved shutters at every penetrated floor shall be permitted.The shutters shall be of noncombustible construction and have a fire-resistance rating of not less than 1.5 hours.The shutter shall be so constructed as to close immediately upon the actuation of a smoke detector installed in accordance with FBC Section 907.3.1 and shall completely shut off the well opening. Escalators shall cease operation when the shutter begins to close. The shutter shall operate at a speed of not more than 30 feet per minute and shall be equipped with a sensitive leading edge to arrest its progress where in contact with any obstacle, and to continue its progress on release there from (FBC Section 712.1.3.2).");
                    }
                    else if (isVOpening2.Checked)
                    {
                        if (VOA == true)
                        {
                            if (numVOLevels == 2)
                            {
                                this.FindAndReplace(wordApp, "VASSEMBLY2", "The vertical opening located on VOFloorBot to VFloorTop will be designed as a two story opening and must be designed in accordance with all criteria in FBC 712.1.9 and FFPC, NFPA 101 Section 12.3 (3).");
                                this.FindAndReplace(wordApp, "VO1", "Assembly occupancies protected by an approved, supervised automatic sprinkler system in accordance with Section 9.7 shall be permitted to have unprotected openings between any two adjacent floors, provided that such openings are separated from unprotected vertical openings serving other floors by a barrier complying with 8.6.5");
                            }
                            else if (numVOLevels > 2 && numVOLevels <= 4)
                            {
                                this.FindAndReplace(wordApp, "VO1", "Assembly occupancies protected by an approved, supervised automatic sprinkler system in accordance with Section 9.7 shall be permitted to have unprotected openings between any two adjacent floors, provided that such openings are separated from unprotected vertical openings serving other floors by a barrier complying with 8.6.5" + "NOTE: Further discussion required regarding a code compliant method of separating the Vertical Opening to create vertical openings not greater than 2 floors.");
                            }
                        }
                        else if (VOR2 == true || VOR1 == true)
                        {
                            if (numVOLevels == 2)
                            {
                                this.FindAndReplace(wordApp, "VO8.6.9.1", "The vertical opening located on floors VOFloorBot to VOFloorTop will be designed as a Convenience Opening and must meet be designed in accordance with all criteria in FBC Section 712.1.9 and FFPC, NFPA 101 Section 8.6.9.1.");
                                this.FindAndReplace(wordApp, "VO1", "(1) Such openings shall connect not more than two adjacent stories (one floor pierced only).");
                                this.FindAndReplace(wordApp, "VO2", "(2) Such openings shall be separated from unprotected vertical openings serving other floors by a barrier complying with 8.6.5.");
                                this.FindAndReplace(wordApp, "VO3", "(3) Such openings shall be separated from corridors.");
                                this.FindAndReplace(wordApp, "VO4", "(4) In new construction, the area of the floor openings shall not exceed twice the horizontal projected area of the stairway.");
                                this.FindAndReplace(wordApp, "VO5", "(5) For new construction, such openings shall not connect more than four contiguous stories, unless otherwise permitted by Chapters 11 through 43.");
                                this.FindAndReplace(wordApp, "VO6", "");
                            }
                        }

                        else if (VOB == true)
                        {
                            if (numVOLevels == 2)
                            {
                                //NEED USER INPUT - IF LOWER LEVELS AND REQUIRED FOR EGRESS THEN BELOW
                                this.FindAndReplace(wordApp, "VO38.2.4.6", "Business occupancies providing a single means of egress for a two-story single-tenant space or building in accordance with NFPA 101 38.2.4.6 shall meet the following criteria:");
                                this.FindAndReplace(wordApp, "VO38.2.4.6P1", "The building is protected throughout by an approved, supervised automatic sprinkler system in accordance with 9.7.1.1(1).");
                                this.FindAndReplace(wordApp, "VO38.2.4.6P2", "The total travel to the outside does not exceed 100 ft (30m).");
                                this.FindAndReplace(wordApp, "VO1019.1", "Exit access stairways and ramps serving as an exit access component in a means of egress system shall comply with the requirements of this section. The number of stories connected by exit access stairways and ramps shall include basements, but not mezzanines. (FBC Section 1019.1)");
                                this.FindAndReplace(wordApp, "VO1019.1P1", "According to FBC Section 1019.1, (in other than Group I-2 and I-3 occupancies)  floor openings containing exit access stairways or ramps that do not comply with one of the conditions listed in this section shall be enclosed with a shaft enclosure constructed in accordance with FBC Section 713.");
                                this.FindAndReplace(wordApp, "VO1019.1P2", "Exit access stairways and ramps that serve or atmospherically communicate between only two stories. Such interconnected stories shall not be open to other stories.");
                            }
                            if (numVOLevels == 2) //AND NOT REQUIRED FOR EGRESS
                            {
                                this.FindAndReplace(wordApp, "VO8.6.9.1", "The vertical opening located on floors VOFloorBot to VOFloorTop will be designed as a Convenience Opening and must meet be designed in accordance with all criteria in FBC Section 712.1.9 and FFPC, NFPA 101 Section 8.6.9.1.");
                                this.FindAndReplace(wordApp, "VO1", "(1) Such openings shall connect not more than two adjacent stories (one floor pierced only).");
                                this.FindAndReplace(wordApp, "VO2", "(2) Such openings shall be separated from unprotected vertical openings serving other floors by a barrier complying with 8.6.5.");
                                this.FindAndReplace(wordApp, "VO3", "(3) Such openings shall be separated from corridors.");
                                this.FindAndReplace(wordApp, "VO4", "(4) In new construction, the area of the floor openings shall not exceed twice the horizontal projected area of the stairway.");
                                this.FindAndReplace(wordApp, "VO5", "(5) For new construction, such openings shall not connect more than four contiguous stories, unless otherwise permitted by Chapters 11 through 43.");
                                this.FindAndReplace(wordApp, "VO6", "(6) Such opening shall not serve as a required means of egress.");
                            }
                            else if (numVOLevels > 2 && numVOLevels <= 4) //AND NOT REQUIRED FOR EGRESS
                            {
                                this.FindAndReplace(wordApp, "VO8.6.9.1", "The vertical opening located on floors VOFloorBot to VOFloorTop will be designed as a Convenience Opening and must meet be designed in accordance with all criteria in FBC Section 712.1.9 and FFPC, NFPA 101 Section 8.6.9.1." + "NOTE: Further discussion required regarding a code compliant method of separating the Vertical Opening to create vertical openings not greater than 2 floors.");
                                this.FindAndReplace(wordApp, "VO1", "(1) Such openings shall connect not more than two adjacent stories (one floor pierced only).");
                                this.FindAndReplace(wordApp, "VO2", "(2) Such openings shall be separated from unprotected vertical openings serving other floors by a barrier complying with 8.6.5.");
                                this.FindAndReplace(wordApp, "VO3", "(3) Such openings shall be separated from corridors.");
                                this.FindAndReplace(wordApp, "VO4", "(4) In new construction, the area of the floor openings shall not exceed twice the horizontal projected area of the stairway.");
                                this.FindAndReplace(wordApp, "VO5", "(5) For new construction, such openings shall not connect more than four contiguous stories, unless otherwise permitted by Chapters 11 through 43.");
                                this.FindAndReplace(wordApp, "VO6", "");
                            }
                        }
                        else if (VOM == true)
                        {
                            if(numVOLevels == 2)
                            {
                                this.FindAndReplace(wordApp, "VO8.6.9.2", "The vertical opening located on floors VOFloorBot to VOFloorTop will be designed as a Convenience Opening and must meet be designed in accordance with all criteria in FBC Section 712.1.9 and FFPC, NFPA 101 Section 8.6.9.2.");
                                this.FindAndReplace(wordApp, "VO1", "(1) The convenience stair openings shall not serve as required means of egress.");
                                this.FindAndReplace(wordApp, "VO2", "(2) The building shall be protected throughout by an approved, supervised automatic sprinkler systems in accordance with Section 9.7");
                                this.FindAndReplace(wordApp, "VO3", "(3) The convenience stair openings shall be protected in accordance with the method detailed for the protection of vertical openings in NFPA 13.");
                                this.FindAndReplace(wordApp, "VO4", "(4) In new construction, the area of the floor openings shall not exceed twice the horizontal projected area of the stairway.");
                                this.FindAndReplace(wordApp, "VO5", "(5) For new construction, such openings shall not connect more than four contiguous stories, unless otherwise permitted by Chapters 11 through 43.");
                                this.FindAndReplace(wordApp, "VO6", "");
                            }
                        }
                    }
                }

                Microsoft.Office.Interop.Word.Table table18 = myWordDoc.Tables[18];
                //LOW LEVEL EXIT SIGNAGE
                if (R1 == true)
                {
                    this.FindAndReplace(wordApp, "LOWEXIT", "NOTE:   FBC Section 1013.2 requires floor-level exit signs in all R-1 (Hotel) Occupancies.  The bottom of the sign shall not be less than 10 inches and no more than 12 inches above the floor.  The sign shall be flush mounted to the door or wall.  The edge of the sign shall be within 4 inches of the door frame on the latch side.");
                    //FIRE ALARM SECTION HOTEL
                    this.FindAndReplace(wordApp, "HOTELFA", "In Hotel (R-1)occupancies, a certain number of rooms must be provided with visible alarms depending on the total number of sleeping rooms in the hotel in accordance with FBC Table 907.5.2.3.2.");
                }
                else
                {
                    table18.Delete();
                }

                Microsoft.Office.Interop.Word.Table table2AndAHalf = myWordDoc.Tables[3];
                table2AndAHalf.Delete();
            }

        }


        private void button1_Click(object sender, EventArgs e)
        {
            CreateWordDocument(@"C:\Users\Owner\Desktop\SLS\SLSFindReplace\Narrative Template.docx", @"C:\Users\alexa\Downloads\DDMMYY_SLS XXXX_Project Name_FPLS Narrative_7th Edit. Code_Template 2020 output.docx");
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label1_Click_2(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void PreviousButton_Click(object sender, EventArgs e)
        {
            if (panelIndex > 0)
            {
                listPanel[--panelIndex].BringToFront();
            }
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            if (panelIndex < listPanel.Count - 1)
            {
                listPanel[++panelIndex].BringToFront();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            listPanel.Add(panel1);
            listPanel.Add(panel2);
            listPanel.Add(panel3);
            listPanel.Add(panel4);
            listPanel[panelIndex].BringToFront();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpeg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";

                if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    SFSDImage.ImageLocation = imageLocation;

                }


            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uploadNFSD_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpeg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    NFSDImage.ImageLocation = imageLocation;

                }


            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uploadESFD_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpeg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    EFSDImage.ImageLocation = imageLocation;

                }


            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void uploadWFSD_Click(object sender, EventArgs e)
        {
            String imageLocation = "";
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "jpeg files(*.jpg)|*.jpg| PNG files(*.png)|*.png| All files(*.*)|*.*";

                if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    imageLocation = dialog.FileName;

                    WFSDImage.ImageLocation = imageLocation;

                }


            }
            catch (Exception)
            {
                MessageBox.Show("An Error Occured", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label27_Click(object sender, EventArgs e)
        {

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
