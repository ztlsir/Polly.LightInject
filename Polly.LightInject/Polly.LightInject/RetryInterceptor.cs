using System;
using System.Linq;
using System.Threading;
using LightInject.Interception;

namespace Polly.LightInject
{
    public class RetryInterceptor : IInterceptor
    {
        public object Invoke(IInvocationInfo invocationInfo)
        {
            var retryableAttribute = invocationInfo.TargetMethod.GetCustomAttributes(typeof(RetryableAttribute), true).FirstOrDefault() as RetryableAttribute;
            if (retryableAttribute != null)
            {
                var policy = Policy
                    .Handle<Exception>(handledException =>
                        retryableAttribute.Vlaue.Exists(exceptionType => handledException.GetType() == exceptionType))
                    .Retry(retryableAttribute.MaxAttempts, (exception, retryCount, context) =>
                    {
                        Thread.Sleep(retryableAttribute.Delay 
                                     * int.Parse(Math.Pow(retryableAttribute.Multiplier, retryCount - 1).ToString()));
                    });

                object proceedResult = null;

                policy.Execute(() => proceedResult = invocationInfo.Proceed());

                return proceedResult;
            }

            return invocationInfo.Proceed();
        }
    }
}
