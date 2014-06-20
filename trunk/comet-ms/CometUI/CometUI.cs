﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Forms;
using CometUI.Properties;
using CometUI.SettingsUI;

namespace CometUI
{
    public partial class CometUI : Form
    {
        public Dictionary<String, CometParam> CometParams = new Dictionary<string, CometParam>();

        private readonly List<RunSearchBackgroundWorker> _runSearchWorkers = 
            new List<RunSearchBackgroundWorker>();

        public CometUI()
        {
            InitializeComponent();

            UpdateCometParams(Settings.Default);
        }

        public void UpdateCometParams(Settings settings)
        {
            // Todo: In the future, make this more effience by only updating the parameters that actually changed.
            if (CometParams.Count > 0)
            {
                CometParams.Clear();
            }

            var dbName = settings.ProteomeDatabaseFile;
            CometParams.Add("database_name", 
                            new TypedCometParam<String>(CometParamType.String,
                                                        dbName, 
                                                        dbName));

            var searchType = settings.SearchType;
            CometParams.Add("decoy_search", 
                            new TypedCometParam<int>(CometParamType.Int, 
                                                     searchType.ToString(CultureInfo.InvariantCulture), 
                                                     searchType));

            var decoyPrefix = settings.DecoyPrefix;
            CometParams.Add("decoy_prefix", 
                            new TypedCometParam<String>(CometParamType.String, 
                                                        decoyPrefix, 
                                                        decoyPrefix));

            var nucleotideReadingFrame = settings.NucleotideReadingFrame;
            CometParams.Add("nucleotide_reading_frame", 
                            new TypedCometParam<int>(CometParamType.Int, 
                                                     nucleotideReadingFrame.ToString(CultureInfo.InvariantCulture), 
                                                     nucleotideReadingFrame));

            var outputPepXMLFile = settings.OutputFormatPepXML ? 1 : 0;
            CometParams.Add("output_pepxmlfile",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     outputPepXMLFile.ToString(CultureInfo.InvariantCulture),
                                                     outputPepXMLFile));

            var outputPinXMLFile = settings.OutputFormatPinXML ? 1 : 0;
            CometParams.Add("output_pinxmlfile",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     outputPinXMLFile.ToString(CultureInfo.InvariantCulture),
                                                     outputPinXMLFile));

            var outputTextFile = settings.OutputFormatTextFile ? 1 : 0;
            CometParams.Add("output_txtfile",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     outputTextFile.ToString(CultureInfo.InvariantCulture),
                                                     outputTextFile));

            var outputSqtFile = settings.OutputFormatSqtFile ? 1 : 0;
            CometParams.Add("output_sqtfile",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     outputSqtFile.ToString(CultureInfo.InvariantCulture),
                                                     outputSqtFile));

            var outputOutFile = settings.OutputFormatOutFiles ? 1 : 0;
            CometParams.Add("output_outfiles",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     outputOutFile.ToString(CultureInfo.InvariantCulture),
                                                     outputOutFile));

            var printExpectScore = settings.PrintExpectScoreInPlaceOfSP ? 1 : 0;
            CometParams.Add("print_expect_score",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     printExpectScore.ToString(CultureInfo.InvariantCulture),
                                                     printExpectScore));

            var showFragmentIons = settings.OutputFormatShowFragmentIons ? 1 : 0;
            CometParams.Add("show_fragment_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     showFragmentIons.ToString(CultureInfo.InvariantCulture),
                                                     showFragmentIons));

            var skipResearching = settings.OutputFormatSkipReSearching ? 1 : 0;
            CometParams.Add("skip_researching",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     skipResearching.ToString(CultureInfo.InvariantCulture),
                                                     skipResearching));

            var numOutputLines = settings.NumOutputLines;
            CometParams.Add("num_output_lines",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     numOutputLines.ToString(CultureInfo.InvariantCulture),
                                                     numOutputLines));

            var searchEnzymeNumber = settings.SearchEnzymeNumber;
            CometParams.Add("search_enzyme_number",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     searchEnzymeNumber.ToString(CultureInfo.InvariantCulture),
                                                     searchEnzymeNumber));

            var sampleEnzymeNumber = settings.SampleEnzymeNumber;
            CometParams.Add("sample_enzyme_number",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     sampleEnzymeNumber.ToString(CultureInfo.InvariantCulture),
                                                     sampleEnzymeNumber));

            var allowedMissedCleavages = settings.AllowedMissedCleavages;
            CometParams.Add("allowed_missed_cleavage",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     allowedMissedCleavages.ToString(CultureInfo.InvariantCulture),
                                                     allowedMissedCleavages));

            var enzymeTermini = settings.EnzymeTermini;
            CometParams.Add("num_enzyme_termini",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     enzymeTermini.ToString(CultureInfo.InvariantCulture),
                                                     enzymeTermini));

            var precursorMassTol = settings.PrecursorMassTolerance;
            CometParams.Add("peptide_mass_tolerance", 
                            new TypedCometParam<double>(CometParamType.Double, 
                                                        precursorMassTol.ToString(CultureInfo.InvariantCulture), 
                                                        precursorMassTol));

            var precursorMassUnit = settings.PrecursorMassUnit;
            CometParams.Add("peptide_mass_units",
                            new TypedCometParam<String>(CometParamType.String,
                                                        precursorMassUnit,
                                                        precursorMassUnit));

            var precursorMassType = settings.PrecursorMassType;
            CometParams.Add("mass_type_parent",
                            new TypedCometParam<String>(CometParamType.String,
                                                        precursorMassType,
                                                        precursorMassType));

            var precursorTolType = settings.PrecursorToleranceType;
            CometParams.Add("precursor_tolerance_type",
                            new TypedCometParam<String>(CometParamType.String,
                                                        precursorTolType,
                                                        precursorTolType));

            var isotopeError = settings.PrecursorIsotopeError;
            CometParams.Add("isotope_error",
                            new TypedCometParam<String>(CometParamType.String,
                                                        isotopeError,
                                                        isotopeError));

            var fragmentBinSize = settings.FragmentBinSize;
            CometParams.Add("fragment_bin_tol",
                            new TypedCometParam<double>(CometParamType.Double,
                                                        fragmentBinSize.ToString(CultureInfo.InvariantCulture),
                                                        fragmentBinSize));

            var fragmentBinOffset = settings.FragmentBinOffset;
            CometParams.Add("fragment_bin_offset",
                            new TypedCometParam<double>(CometParamType.Double,
                                                        fragmentBinOffset.ToString(CultureInfo.InvariantCulture),
                                                        fragmentBinOffset));

            var fragmentMassType = settings.FragmentMassType;
            CometParams.Add("mass_type_fragment",
                            new TypedCometParam<String>(CometParamType.String,
                                                        fragmentMassType,
                                                        fragmentMassType));

            var useSparseMatrix = settings.UseSparseMatrix ? 1 : 0;
            CometParams.Add("use_sparse_matrix",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useSparseMatrix.ToString(CultureInfo.InvariantCulture),
                                                     useSparseMatrix));

            var useAIons = settings.UseAIons ? 1 : 0;
            CometParams.Add("use_A_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useAIons.ToString(CultureInfo.InvariantCulture),
                                                     useAIons));

            var useBIons = settings.UseBIons ? 1 : 0;
            CometParams.Add("use_B_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useBIons.ToString(CultureInfo.InvariantCulture),
                                                     useBIons));

            var useCIons = settings.UseCIons ? 1 : 0;
            CometParams.Add("use_C_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useCIons.ToString(CultureInfo.InvariantCulture),
                                                     useCIons));

            var useXIons = settings.UseXIons ? 1 : 0;
            CometParams.Add("use_X_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useXIons.ToString(CultureInfo.InvariantCulture),
                                                     useXIons));

            var useYIons = settings.UseYIons ? 1 : 0;
            CometParams.Add("use_Y_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useYIons.ToString(CultureInfo.InvariantCulture),
                                                     useYIons));

            var useZIons = settings.UseZIons ? 1 : 0;
            CometParams.Add("use_Z_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useZIons.ToString(CultureInfo.InvariantCulture),
                                                     useZIons));

            var useFlankIons = settings.TheoreticalFragmentIons ? 1 : 0;
            CometParams.Add("theoretical_fragment_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useFlankIons.ToString(CultureInfo.InvariantCulture),
                                                     useFlankIons));

            var useNLIons = settings.UseNLIons ? 1 : 0;
            CometParams.Add("use_NL_ions",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     useNLIons.ToString(CultureInfo.InvariantCulture),
                                                     useNLIons));

            foreach (var item in settings.StaticMods)
            {
                string[] staticMods = item.Split(',');
                
                string paramName = GetStaticModParamName(staticMods[1]);
                double massDiff = Convert.ToDouble(staticMods[2]);
                CometParams.Add(paramName,
                                new TypedCometParam<double>(CometParamType.Double,
                                                            massDiff.ToString(CultureInfo.InvariantCulture),
                                                            massDiff));
            }

            var cTermPeptideMass = settings.StaticModCTermPeptide;
            CometParams.Add("add_Cterm_peptide",
                new TypedCometParam<double>(CometParamType.Double,
                                            cTermPeptideMass.ToString(CultureInfo.InvariantCulture),
                                            cTermPeptideMass));

            var nTermPeptideMass = settings.StaticModNTermPeptide;
            CometParams.Add("add_Nterm_peptide",
                new TypedCometParam<double>(CometParamType.Double,
                                            nTermPeptideMass.ToString(CultureInfo.InvariantCulture),
                                            nTermPeptideMass));

            var cTermProteinMass = settings.StaticModCTermProtein;
            CometParams.Add("add_Cterm_protein",
                new TypedCometParam<double>(CometParamType.Double,
                                            cTermProteinMass.ToString(CultureInfo.InvariantCulture),
                                            cTermProteinMass));

            var nTermProteinMass = settings.StaticModNTermProtein;
            CometParams.Add("add_Nterm_protein",
                new TypedCometParam<double>(CometParamType.Double,
                                            nTermProteinMass.ToString(CultureInfo.InvariantCulture),
                                            nTermProteinMass));

            int modNum = 0;
            foreach (var item in settings.VariableMods)
            {
                modNum++;
                string paramName = "variable_mod" + modNum;
                string[] varModsStr = item.Split(',');
                var varMods = new VarMods(varModsStr[0],
                                          Convert.ToDouble(varModsStr[1]),
                                          Convert.ToInt32(varModsStr[2]),
                                          Convert.ToInt32(varModsStr[3]));
                var varModsStrValue = varMods.VarModMass + " " + varMods.VarModChar + " " + varMods.BinaryMod+ " " + varMods.MaxNumVarModAAPerMod;
                CometParams.Add(paramName,
                                new TypedCometParam<VarMods>(CometParamType.VarMods,
                                                             varModsStrValue,
                                                             varMods));

            }

            var varCTerminus = settings.VariableCTerminus;
            CometParams.Add("variable_C_terminus",
                new TypedCometParam<double>(CometParamType.Double,
                                            varCTerminus.ToString(CultureInfo.InvariantCulture),
                                            varCTerminus));

            var varCTerminusDist = settings.VariableCTermDistance;
            CometParams.Add("variable_C_terminus_distance",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     varCTerminusDist.ToString(CultureInfo.InvariantCulture),
                                                     varCTerminusDist));

            var varNTerminus = settings.VariableNTerminus;
            CometParams.Add("variable_N_terminus",
                new TypedCometParam<double>(CometParamType.Double,
                                            varNTerminus.ToString(CultureInfo.InvariantCulture),
                                            varNTerminus));

            var varNTerminusDist = settings.VariableNTermDistance;
            CometParams.Add("variable_N_terminus_distance",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     varNTerminusDist.ToString(CultureInfo.InvariantCulture),
                                                     varNTerminusDist));

            var maxVarModsInPeptide = settings.MaxVarModsInPeptide;
            CometParams.Add("max_variable_mods_in_peptide",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     maxVarModsInPeptide.ToString(CultureInfo.InvariantCulture),
                                                     maxVarModsInPeptide));

            var mzxmlScanRange = new IntRange(settings.mzxmlScanRangeMin, settings.mzxmlScanRangeMax);
            string mzxmlScanRangeString = mzxmlScanRange.Start.ToString(CultureInfo.InvariantCulture)
                                          + " " + mzxmlScanRange.End.ToString(CultureInfo.InvariantCulture);
            CometParams.Add("scan_range", 
                            new TypedCometParam<IntRange>(CometParamType.IntRange, 
                                                          mzxmlScanRangeString,
                                                          mzxmlScanRange));

            var mzxmlPrecursorChargeRange = new IntRange(settings.mzxmlPrecursorChargeRangeMin, settings.mzxmlPrecursorChargeRangeMax);
            string mzxmlPrecursorChargeRageString = mzxmlPrecursorChargeRange.Start.ToString(CultureInfo.InvariantCulture)
                                          + " " + mzxmlPrecursorChargeRange.End.ToString(CultureInfo.InvariantCulture);
            CometParams.Add("precursor_charge",
                            new TypedCometParam<IntRange>(CometParamType.IntRange,
                                                          mzxmlPrecursorChargeRageString,
                                                          mzxmlPrecursorChargeRange));

            var mzxmlMSLevel = settings.mzxmlMsLevel;
            CometParams.Add("ms_level",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     mzxmlMSLevel.ToString(CultureInfo.InvariantCulture),
                                                     mzxmlMSLevel));

            var mzxmlActivationMethod = settings.mzxmlActivationMethod;
            CometParams.Add("activation_method",
                            new TypedCometParam<String>(CometParamType.String,
                                                        mzxmlActivationMethod,
                                                        mzxmlActivationMethod));

            var minPeaks = settings.spectralProcessingMinPeaks;
            CometParams.Add("minimum_peaks",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     minPeaks.ToString(CultureInfo.InvariantCulture),
                                                     minPeaks));

            var minIntensity = settings.spectralProcessingMinIntensity;
            CometParams.Add("minimum_intensity",
                new TypedCometParam<double>(CometParamType.Double,
                                            minIntensity.ToString(CultureInfo.InvariantCulture),
                                            minIntensity));

            var removePrecursorTol = settings.spectralProcessingRemovePrecursorTol;
            CometParams.Add("remove_precursor_tolerance",
                new TypedCometParam<double>(CometParamType.Double,
                                            removePrecursorTol.ToString(CultureInfo.InvariantCulture),
                                            removePrecursorTol));

            var removePrecursorPeak = settings.spectralProcessingRemovePrecursorPeak;
            CometParams.Add("remove_precursor_peak",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     removePrecursorPeak.ToString(CultureInfo.InvariantCulture),
                                                     removePrecursorPeak));

            var clearMzRange = new DoubleRange(settings.spectralProcessingClearMzMin,
                                               settings.spectralProcessingClearMzMax);
            string clearMzRangeString = clearMzRange.Start.ToString(CultureInfo.InvariantCulture)
                                          + " " + clearMzRange.End.ToString(CultureInfo.InvariantCulture);
            CometParams.Add("clear_mz_range",
                            new TypedCometParam<DoubleRange>(CometParamType.DoubleRange,
                                                             clearMzRangeString,
                                                             clearMzRange));

            var spectrumBatchSize = settings.SpectrumBatchSize;
            CometParams.Add("spectrum_batch_size",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     spectrumBatchSize.ToString(CultureInfo.InvariantCulture),
                                                     spectrumBatchSize));

            var numThreads = settings.NumThreads;
            CometParams.Add("num_threads",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     numThreads.ToString(CultureInfo.InvariantCulture),
                                                     numThreads));

            var numResults = settings.NumResults;
            CometParams.Add("num_results",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     numResults.ToString(CultureInfo.InvariantCulture),
                                                     numResults));

            var maxFragmentCharge = settings.MaxFragmentCharge;
            CometParams.Add("max_fragment_charge",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     maxFragmentCharge.ToString(CultureInfo.InvariantCulture),
                                                     maxFragmentCharge));

            var maxPrecursorCharge = settings.MaxPrecursorCharge;
            CometParams.Add("max_precursor_charge",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     maxPrecursorCharge.ToString(CultureInfo.InvariantCulture),
                                                     maxPrecursorCharge));

            var clipNTermMethionine = settings.ClipNTermMethionine ? 1 : 0;
            CometParams.Add("clip_nterm_methionine",
                            new TypedCometParam<int>(CometParamType.Int,
                                                     clipNTermMethionine.ToString(CultureInfo.InvariantCulture),
                                                     clipNTermMethionine));
        }

        public static String GetStaticModParamName(String aa)
        {
            String paramName = String.Empty;
            switch (aa)
            {
                case "G":
                    paramName = "add_G_glycine";
                    break;
                case "A":
                    paramName = "add_A_alanine";
                    break;
                case "S":
                    paramName = "add_S_serine";
                    break;
                case "P":
                    paramName = "add_P_proline";
                    break;
                case "V":
                    paramName = "add_V_valine";
                    break;
                case "T":
                    paramName = "add_T_threonine";
                    break;
                case "C":
                    paramName = "add_C_cysteine";
                    break;
                case "L":
                    paramName = "add_L_leucine";
                    break;
                case "I":
                    paramName = "add_I_isoleucine";
                    break;
                case "N":
                    paramName = "add_N_asparagine";
                    break;
                case "D":
                    paramName = "add_D_aspartic_acid";
                    break;
                case "Q":
                    paramName = "add_Q_glutamine";
                    break;
                case "K":
                    paramName = "add_K_lysine";
                    break;
                case "E":
                    paramName = "add_E_glutamic_acid";
                    break;
                case "M":
                    paramName = "add_M_methionine";
                    break;
                case "O":
                    paramName = "add_O_ornithine";
                    break;
                case "H":
                    paramName = "add_H_histidine";
                    break;
                case "F":
                    paramName = "add_F_phenylalanine";
                    break;
                case "R":
                    paramName = "add_R_arginine";
                    break;
                case "Y":
                    paramName = "add_Y_tyrosine";
                    break;
                case "W":
                    paramName = "add_W_tryptophan";
                    break;
                case "B":
                    paramName = "add_B_user_amino_acid";
                    break;
                case "J":
                    paramName = "add_J_user_amino_acid";
                    break;
                case "U":
                    paramName = "add_U_user_amino_acid";
                    break;
                case "X":
                    paramName = "add_X_user_amino_acid";
                    break;
                case "Z":
                    paramName = "add_Z_user_amino_acid";
                    break;
            }

            return paramName;
        }

        private void SearchSettingsToolStripMenuItemClick(object sender, EventArgs e)
        {
            var searchSettingsDlg = new SearchSettingsDlg();
            if (DialogResult.OK == searchSettingsDlg.ShowDialog())
            {
                if (searchSettingsDlg.SettingsChanged)
                {
                    UpdateCometParams(Settings.Default);
                }
            }
        }

        private void RunSearchToolStripMenuItemClick(object sender, EventArgs e)
        {
            var runSearchDlg = new RunSearchDlg(this);
            if (DialogResult.OK == runSearchDlg.ShowDialog())
            {
                var runSearchWorker = new RunSearchBackgroundWorker();
                runSearchWorker.DoWork(runSearchDlg);
                _runSearchWorkers.Add(runSearchWorker);
            }
        }

        private void CometUIFormClosing(object sender, FormClosingEventArgs e)
        {
            WorkerThreadsCleanupTimer.Stop();
            WorkerThreadsCleanupTimer.Enabled = false;

            foreach (var worker in _runSearchWorkers)
            {
                worker.CancelAsync();
            }

            _runSearchWorkers.Clear();
        }

        private void WorkerThreadsCleanupTimerTick(object sender, EventArgs e)
        {
            foreach (var worker in _runSearchWorkers)
            {
                if (!worker.IsBusy())
                {
                    _runSearchWorkers.Remove(worker);
                }
            }
        }

        private void CometUILoad(object sender, EventArgs e)
        {
            WorkerThreadsCleanupTimer.Enabled = true;
            WorkerThreadsCleanupTimer.Start();
        }

        private void ExitToolStripMenuItemClick(object sender, EventArgs e)
        {
            Close();
        }

        private void SaveSearchSettingsToolStripMenuItemClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(Resources.CometUI_SaveSearchSettingsToolStripMenuItemClick_Are_you_sure_you_want_to_overwrite_the_current_settings_, Resources.CometUI_SaveSearchSettingsToolStripMenuItemClick_Save_Search_Settings, MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.OK)
            {
                Settings.Default.Save();
            }
        }
    }
}
