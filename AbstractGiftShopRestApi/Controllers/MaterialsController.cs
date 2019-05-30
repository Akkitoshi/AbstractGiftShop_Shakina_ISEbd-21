﻿using AbstractGiftShopServiceDAL.BindingModels;
using AbstractGiftShopServiceDAL.Interfaces;
using System;
using System.Web.Http;

namespace AbstractGiftShopRestApi.Controllers
{
    public class MaterialsController : ApiController
    {
        private readonly IMaterialsService _service;
        public MaterialsController(IMaterialsService service)
        {
            _service = service;
        }
        [HttpGet]
        public IHttpActionResult GetList()
        {
            var list = _service.GetList();
            if (list == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(list);
        }
        [HttpGet]
        public IHttpActionResult Get(int id)
        {
            var element = _service.GetElement(id);
            if (element == null)
            {
                InternalServerError(new Exception("Нет данных"));
            }
            return Ok(element);
        }
        [HttpPost]
        public void AddElement(MaterialsBindingModel model)
        {
            _service.AddElement(model);
        }
        [HttpPost]
        public void UpdElement(MaterialsBindingModel model)
        {
            _service.UpdElement(model);
        }
        [HttpPost]
        public void DelElement(MaterialsBindingModel model)
        {
            _service.DelElement(model.Id);
        }
    }
}