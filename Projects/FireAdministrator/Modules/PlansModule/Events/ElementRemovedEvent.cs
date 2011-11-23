﻿using System;
using Microsoft.Practices.Prism.Events;
using FiresecAPI.Models;
using System.Collections.Generic;

namespace PlansModule.Events
{
    public class ElementRemovedEvent : CompositePresentationEvent<List<ElementBase>>
    {
    }
}