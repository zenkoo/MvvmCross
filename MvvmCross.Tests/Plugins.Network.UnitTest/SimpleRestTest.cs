// SimpleRestTest.cs
// (c) Copyright Cirrious Ltd. http://www.cirrious.com
// MvvmCross is licensed using Microsoft Public License (Ms-PL)
// Contributions and inspirations noted in readme.md and license.txt
//
// Project Lead - Stuart Lodge, @slodge, me@slodge.com

using System;
using System.Net;
using System.Threading.Tasks;
using MvvmCross.Plugins.Json;
using MvvmCross.Plugins.Network.Rest;
using MvvmCross.Plugins.Network.Test.TestClasses.GoogleBooks;
using MvvmCross.Test.Core;
using Xunit;

namespace MvvmCross.Plugins.Network.Test
{
    
    public class SimpleRestTest
        : MvxIoCSupportingTest
    {
        
        public void SetUp()
        {
        }

        [Fact]
        public async Task GetDataFromGoogleBooks()
        {
            ClearAll();

            // not a real test yet....
            var url = BooksService.GetSearchUrl("MonoTouch");

            var json = new MvxJsonConverter();
            var client = new MvxJsonRestClient
            {
                JsonConverterProvider = () => json
            };
            var request = new MvxRestRequest(url);
            MvxDecodedRestResponse<BookSearchResult> theResponse = null;
            Exception exception = null;
            theResponse = await client.MakeRequestForAsync<BookSearchResult>(request);

            Assert.IsNotNull(theResponse);
            Assert.IsNull(exception);
            Assert.IsNotNull(theResponse.Result);
            Assert.Equal(HttpStatusCode.OK, theResponse.StatusCode);
            Assert.IsTrue(theResponse.Result.items.Count == 10);
            Assert.IsTrue(theResponse.Result.items[0].ToString().Contains("MonoTouch"));
        }
    }
}