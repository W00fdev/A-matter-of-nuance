mergeInto(LibraryManager.library, {

  Hello: function () {
    console.log("Hello, world!!!");
  },

  RateGame: function () {
    ysdk.feedback.canReview()
          .then(({ value, reason }) => {
              if (value) {
                  ysdk.feedback.requestReview()
                      .then(({ feedbackSent }) => {
                          console.log(feedbackSent);
                      })
              } else {
                  console.log(reason)
              }
          })
  },

  SaveData : function(data)
  {

  },

  LoadData : function()
  {

  },
});