using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using DotSpatialProjection.Services.Contracts;
using WebRole.Models;
using WebRole.ViewModels;

namespace WebRole.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebRole _webRole;
        private readonly ICoordinateConverterService _coordinateConverterService;
        private static string _numberOfCities;
        private static string _numberOfGenerations;
        private static string _numberOfChromosomes;
        private static string _randomOrCirclePlotting;
        private static string _mutationMethod;
        private static string _crossOverMethod;
        private static string _selectionMethod;
        private static string _results;

        public HomeController(IWebRole webRole, ICoordinateConverterService coordinateConverterService)
        {
            _webRole = webRole;
            _coordinateConverterService = coordinateConverterService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult UpdatedIndex()
        {
            var mainViewModel = CreateMainViewModelWithUpdatedProperties();
            return View("Index", mainViewModel);
        }

        [HttpGet]
        public ActionResult Solve(string[] c)
        {
            UpdateStaticFieldsAndRunInternalProcess(c);
            var bestSolution = GetTheBestSolutionFrom(_results);
            ClearStaticFields();
            return Json(new { results = bestSolution }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Solve(MainViewModel mainViewModel)
        {
            UpdatePropertiesAndRunInternalProcess(mainViewModel);
            _results = GetTheBestSolutionFrom(_results);
            return RedirectToAction("UpdatedIndex");
        }

        [HttpGet]
        public ActionResult ConvertToArmaCoordinates(string[] c)
        {
            var armaCoordinates = new List<ArmaCoordinate>();
            foreach (var coordinate in c)
            {
                var latLong = coordinate.Split(',');
                var latitude = Convert.ToDouble(latLong[0], CultureInfo.InvariantCulture);
                var longitude = Convert.ToDouble(latLong[1], CultureInfo.InvariantCulture);
                var armaCoordinateList = _coordinateConverterService.ConvertToArmaCoordinates(latitude, longitude);
                var armaCoordinate = new ArmaCoordinate {Y = armaCoordinateList[0], X = armaCoordinateList[1]};
                armaCoordinates.Add(armaCoordinate);
            }
            return Json(new { armaCoordinates }, JsonRequestBehavior.AllowGet);
        }

        private void UpdateStaticFieldsAndRunInternalProcess(IEnumerable<string> c)
        {
            _numberOfCities = c.Count().ToString();
            _numberOfGenerations = "1000";
            _numberOfChromosomes = "1000";
            _randomOrCirclePlotting = "3";
            _mutationMethod = "1";
            _crossOverMethod = "1";
            _selectionMethod = "1";
            var coordinates = c.Aggregate(string.Empty, (current, t) => current + (t + " "));
            _results = _webRole.RunInternalProcess(_numberOfCities + " " + _numberOfGenerations + " " +
                                                   _numberOfChromosomes + " " + _randomOrCirclePlotting
                                                   + " " + _mutationMethod + " " + _crossOverMethod + " " + _selectionMethod + " " + coordinates);
        }

        private void UpdatePropertiesAndRunInternalProcess(MainViewModel mainViewModel)
        {
            UpdatePropertiesFrom(mainViewModel);
            _results = _webRole.RunInternalProcess(mainViewModel.SetProcessParamteresFromProperties());
        }

        private static string GetTheBestSolutionFrom(string displayResults)
        {
            var solutions = displayResults.Split('\n');
            var solutionPathDictionary = new Dictionary<double, string>();
            ExtractEachSolutionAndPathAndSaveToDictionary(solutions, solutionPathDictionary);
            var keyList = Sort(solutionPathDictionary);
            return "Solution: " + keyList[0] + " Path: " + solutionPathDictionary[keyList[0]];
        }

        private static void UpdatePropertiesFrom(MainViewModel mainViewModel)
        {
            _numberOfCities = mainViewModel.NumberOfCities;
            _numberOfGenerations = mainViewModel.NumberOfGenerations;
            _numberOfChromosomes = mainViewModel.NumberOfChromosomes;
            _randomOrCirclePlotting = mainViewModel.RandomOrCirclePlotting;
            _mutationMethod = mainViewModel.MutationMethod;
            _crossOverMethod = mainViewModel.CrossOverMethod;
            _selectionMethod = mainViewModel.SelectionMethod;
        }

        private static MainViewModel CreateMainViewModelWithUpdatedProperties()
        {
            var mainViewModel = new MainViewModel()
            {
                NumberOfCities = _numberOfCities,
                NumberOfGenerations = _numberOfGenerations,
                NumberOfChromosomes = _numberOfChromosomes,
                RandomOrCirclePlotting = _randomOrCirclePlotting,
                MutationMethod = _mutationMethod,
                CrossOverMethod = _crossOverMethod,
                SelectionMethod = _selectionMethod,
                Results = _results
            };
            ClearStaticFields();
            return mainViewModel;
        }

        private static void ClearStaticFields()
        {
            _numberOfCities = string.Empty;
            _numberOfGenerations = string.Empty;
            _numberOfChromosomes = string.Empty;
            _randomOrCirclePlotting = string.Empty;
            _mutationMethod = string.Empty;
            _crossOverMethod = string.Empty;
            _selectionMethod = string.Empty;
            _results = string.Empty;
        }

        private static List<double> Sort(Dictionary<double, string> solutionPathDictionary)
        {
            var keyList = solutionPathDictionary.Keys.ToList();
            keyList.Sort();
            return keyList;
        }

        private static void ExtractEachSolutionAndPathAndSaveToDictionary(string[] solutions, Dictionary<double, string> solutionPathDictionary)
        {
            for (var i = 0; i < solutions.Length; i++)
            {
                if (i%2 != 0) continue;
                try
                {
                    solutionPathDictionary[Convert.ToDouble(solutions[i], CultureInfo.InvariantCulture)] = solutions[i + 1];
                }
                catch (Exception)
                {
                    //Ugly indeed, but will have to do
                    break;
                }
            }
        }
    }
}