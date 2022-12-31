using Backend.BusinessLogic.Base;
using Backend.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.BusinessLogic.Images
{
    public class ImageService : BaseService
    {
        public ImageService(ServiceDependencies serviceDependencies) : base(serviceDependencies)
        {
        }

        public byte[] GetImgContent(Guid id)
        {
            var img = UnitOfWork.Images.Get().FirstOrDefault(i => i.Idimg == id);
            if (img == null)
            {
                throw new NotFoundErrorException("image not found");
            }
            return img.ImgContent;
        }
    }
}
