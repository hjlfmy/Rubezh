﻿using System.Collections.Generic;
using FiresecAPI.Models;
using Microsoft.Practices.Prism.Events;

namespace PlansModule.Events
{
    public class ElementChangedEvent : CompositePresentationEvent<List<ElementBase>>
    {
    }
}
