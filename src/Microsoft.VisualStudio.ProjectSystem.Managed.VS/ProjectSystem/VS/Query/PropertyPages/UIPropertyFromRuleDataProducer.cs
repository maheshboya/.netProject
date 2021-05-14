﻿// Licensed to the .NET Foundation under one or more agreements. The .NET Foundation licenses this file to you under the MIT license. See the LICENSE.md file in the project root for more information.

using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.ProjectSystem.Query;
using Microsoft.VisualStudio.ProjectSystem.Query.ProjectModel;
using Microsoft.VisualStudio.ProjectSystem.Query.QueryExecution;

namespace Microsoft.VisualStudio.ProjectSystem.VS.Query
{
    /// <summary>
    /// Handles retrieving a set of <see cref="IUIProperty"/>s from an <see cref="IPropertyPage"/>
    /// or <see cref="ILaunchProfile"/>.
    /// </summary>
    internal class UIPropertyFromRuleDataProducer : QueryDataFromProviderStateProducerBase<ContextAndRuleProviderState>
    {
        private readonly IUIPropertyPropertiesAvailableStatus _properties;

        public UIPropertyFromRuleDataProducer(IUIPropertyPropertiesAvailableStatus properties)
        {
            _properties = properties;
        }

        protected override Task<IEnumerable<IEntityValue>> CreateValuesAsync(IQueryExecutionContext queryExecutionContext, IEntityValue parent, ContextAndRuleProviderState providerState)
        {
            (string versionKey, long versionNumber) = providerState.ProjectState.GetUnconfiguredProjectVersion();
            queryExecutionContext.ReportInputDataVersion(versionKey, versionNumber);

            return Task.FromResult(UIPropertyDataProducer.CreateUIPropertyValues(queryExecutionContext, parent, providerState.ProjectState, providerState.PropertiesContext, providerState.Rule, _properties));
        }
    }
}
