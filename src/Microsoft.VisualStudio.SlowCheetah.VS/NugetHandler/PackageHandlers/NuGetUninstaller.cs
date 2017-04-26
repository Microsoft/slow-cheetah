﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.SlowCheetah.VS
{
    using System;
    using EnvDTE;
    using Microsoft.VisualStudio.ComponentModelHost;
    using NuGet.VisualStudio;

    /// <summary>
    /// Uninstalls older versions of the SlowCheetah NuGet package
    /// </summary>
    internal class NuGetUninstaller : PackageHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NuGetUninstaller"/> class.
        /// </summary>
        /// <param name="package">VS package</param>
        public NuGetUninstaller(IServiceProvider package)
            : base(package)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NuGetUninstaller"/> class.
        /// </summary>
        /// <param name="successor">The successor with the same package</param>
        public NuGetUninstaller(PackageHandler successor)
            : base(successor)
        {
        }

        /// <inheritdoc/>
        internal override void Execute(Project project)
        {
            var componentModel = (IComponentModel)this.Package.GetService(typeof(SComponentModel));
            IVsPackageUninstaller packageUninstaller = componentModel.GetService<IVsPackageUninstaller>();
            packageUninstaller.UninstallPackage(project, SlowCheetahNuGetManager.OldPackageName, true);

            this.Successor.Execute(project);
        }
    }
}
