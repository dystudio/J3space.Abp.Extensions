﻿using J3space.Abp.Account.Localization;
using Volo.Abp.Localization;
using Volo.Abp.Settings;

namespace J3space.Abp.Account.Settings
{
    public class AbpAccountSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            context.Add(
                new SettingDefinition(
                    AbpAccountSettingNames.IsSelfRegistrationEnabled,
                    "true",
                    L("DisplayName:Abp.Account.IsSelfRegistrationEnabled"),
                    L("Description:Abp.Account.IsSelfRegistrationEnabled"), isVisibleToClients : true)
            );

            context.Add(
                new SettingDefinition(
                    AbpAccountSettingNames.EnableLocalLogin,
                    "true",
                    L("DisplayName:Abp.Account.EnableLocalLogin"),
                    L("Description:Abp.Account.EnableLocalLogin"), isVisibleToClients : true)
            );
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<AbpAccountResource>(name);
        }
    }
}