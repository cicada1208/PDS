using FluentValidation;
using System;

namespace Lib
{
    public class RuleUtil
    {
    }

    public static class RuleExUtil
    {
        /// <summary>
        /// 自定義最大字長限制(適用中文全型)
        /// </summary>
        /// <typeparam name="T">Model or ViewMdel Type</typeparam>
        /// <typeparam name="TProperty">Property Type</typeparam>
        /// <param name="maxLenProvider">取得最大字長</param>
        /// <returns></returns>
        public static IRuleBuilderOptions<T, TProperty> MaxLen<T, TProperty>
            (this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, int> maxLenProvider)
        {
            // rootObject: is Model or ViewMdel
            return ruleBuilder.Must((rootObject, propertyVal, context) =>
            {
                int maxLen = maxLenProvider(rootObject);
                int propertyValLen = (propertyVal as string).StrLen();

                context.MessageFormatter
                  .AppendArgument("MaxLen", maxLen)
                  .AppendArgument("PropertyValLen", propertyValLen);

                return propertyValLen <= maxLen;
            })
            .WithMessage("'{PropertyName}' 字長限制{MaxLen}個字符。已達{PropertyValLen}個字符。");
        }
    }

}
