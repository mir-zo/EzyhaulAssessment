﻿using EzyhaulAssessment.Core.Services;
using MvvmCross.ViewModels;
using Refit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EzyhaulAssessment.Core.ViewModels
{
	public class TipViewModel : MvxViewModel
	{
		readonly ICalculationService _calculationService;
		//IServerApiService serverApiService;
		IServerApiService _serverApiService;

		public TipViewModel(ICalculationService calculationService, IServerApiService serverApiService)
		{
			_calculationService = calculationService;
			_serverApiService = serverApiService;
			Height = 35;
		}



		public async override void ViewAppeared()
		{
			base.ViewAppeared();
			try
			{
				var str = await _serverApiService.GetJobInfo();
			}
			catch (Exception ex)
			{

			}
		}


		public override async Task Initialize()
		{
			await base.Initialize();
		


			_subTotal = 100;
			_generosity = 10;

			Recalculate();
		}

		private double _subTotal;
		public double SubTotal
		{
			get => _subTotal;
			set
			{
				_subTotal = value;
				RaisePropertyChanged(() => SubTotal);

				Recalculate();
			}
		}

		private int _generosity;
		public int Generosity
		{
			get => _generosity;
			set
			{
				_generosity = value;
				RaisePropertyChanged(() => Generosity);

				Recalculate();
			}
		}

		private double _tip;
		public double Tip
		{
			get => _tip;
			set
			{
				_tip = value;
				RaisePropertyChanged(() => Tip);
			}
		}

		
		private double _Height;
		public double Height
		{
			get => _Height;
			set
			{
				_Height = value;
				RaisePropertyChanged(() => Height);
			}
		}



		private void Recalculate()
		{
			Tip = _calculationService.TipAmount(SubTotal, Generosity);
		}
	}
}
