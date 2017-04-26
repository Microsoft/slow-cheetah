﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE file in the project root for full license information.

namespace Microsoft.VisualStudio.SlowCheetah.VS
{
    using System;
    using EnvDTE;
    using Microsoft.VisualStudio.ComponentModelHost;
    using NuGet.VisualStudio;

    /// <summary>
    /// Installs the latest SlowCheetah NuGet package
    /// </summary>
    internal class NugetInstaller : PackageHandler
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NugetInstaller"/> class.
        /// </summary>
        /// <param name="package">Vs package</param>
        public NugetInstaller(IServiceProvider package)
            : base(package)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetInstaller"/> class.
        /// </summary>
        /// <param name="successor">The successor with the same package</param>
        public NugetInstaller(PackageHandler successor)
            : base(successor)
        {
        }

        /// <inheritdoc/>
        internal override void Execute(Project project)
        {
            var componentModel = (IComponentModel)this.Package.GetService(typeof(SComponentModel));
            IVsPackageInstaller2 packageInstaller = componentModel.GetService<IVsPackageInstaller2>();
            packageInstaller.InstallLatestPackage(null, project, SlowCheetahNuGetManager.PackageName, true, false);

            this.Successor.Execute(project);
        }
    }
}
