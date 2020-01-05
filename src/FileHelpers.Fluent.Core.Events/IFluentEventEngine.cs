using System;
using System.Collections.Generic;
using System.Text;

namespace FileHelpers.Fluent.Core.Events
{
    public interface IFluentEventEngine
    {
        event FluentEventHandler BeforeReadRecord;

        event FluentEventHandler AfterReadRecord;

        event FluentEventHandler BeforeWriteRecord;

        event FluentEventHandler AfterWriteRecord;
    }
}
