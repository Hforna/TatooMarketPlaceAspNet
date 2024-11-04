﻿using FileTypeChecker.Extensions;
using FileTypeChecker.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TatooMarket.Application.Extensions
{
    public static class ImageValidator
    {
        public static (bool isImage, string fileName) Validate(Stream file)
        {
            (bool isImage, string fileName) = (false, "");

            if(file.Is<JointPhotographicExpertsGroup>())
            {
                isImage = true;
                fileName = $"{Guid.NewGuid()}{JointPhotographicExpertsGroup.TypeExtension}";
            } else if(file.Is<PortableNetworkGraphic>())
            {
                isImage = true;
                fileName = $"{Guid.NewGuid()}{PortableNetworkGraphic.TypeExtension}";
            }

            file.Position = 0;

            return (isImage, fileName);
        }
    }
}
