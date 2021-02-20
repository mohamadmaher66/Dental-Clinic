﻿using Infrastructure;
using System;
using System.Linq;
using DBContext;
using DTOs;
using System.Collections.Generic;
using AutoMapper;
using Request;
using ClinicModule;
using Enums;

namespace ExpenseModule
{
    public class ExpenseDSL
    {
        ExpenseRepository expenseRepository;
        UnitOfWork UoW;
        IMapper mapper;

        public ExpenseDSL(IMapper _mapper)
        {
            UoW = new UnitOfWork(new DentalClinicDBContext());
            expenseRepository = new ExpenseRepository(UoW, _mapper);
            mapper = _mapper;
        }

        public List<ExpenseDTO> GetAll(GridSettings gridSettings)
        {
            try
            {
                return expenseRepository.GetAll(gridSettings).ToList();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ExpenseDTO GetById(int expenseId)
        {
            try
            {
                return expenseRepository.GetById(expenseId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Add(ExpenseDTO expense, int expenseId)
        {
            try
            {
                expenseRepository.Add(expense, expenseId);
                UoW.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(ExpenseDTO expense, int expenseId)
        {
            try
            {
                expenseRepository.Update(expense, expenseId);
                UoW.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public List<ExpenseDTO> Delete(ExpenseDTO expense, GridSettings gridSettings)
        {
            try
            {
                expenseRepository.Delete(expense);
                UoW.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
            }
            return GetAll(gridSettings);
        }

        public List<DetailsList> GetDetailsLists()
        {
            try
            {
                List<ClinicDTO> clinicList = new ClinicDSL(mapper).GetAllLite();
                DetailsList donorsList = new DetailsList()
                {
                    DetailsListId = (int)DetailsListEnum.Clinic,
                    List = clinicList
                };
                List<DetailsList> detailsList = new List<DetailsList>();
                detailsList.Add(donorsList);

                return detailsList;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}