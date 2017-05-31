using System;

namespace Bshkara.Core.Base
{
    public interface IIdentityEntity
    {
        Guid Id { get; set; }

        bool IsDeleted { get; set; }
    }
}