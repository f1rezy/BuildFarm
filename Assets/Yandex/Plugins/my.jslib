mergeInto(LibraryManager.library, {

  SaveExtern: function (data) {
    var dataString = UTF8ToString(data);
    var myObject = JSON.parse(dataString);
    
    player.setData(myObject).then(() => {
      console.log("data is set")
    });
  },

  LoadExtern: function () {
    player.getData().then(_data => {
      const myJSON = JSON.stringify(_data);
      myGameInstance.SendMessage('Progress', 'Load', myJSON);
    });
  },

  AddCoinsExtern: function (value) {
    ysdk.adv.showRewardedVideo({
    callbacks: {
        onOpen: () => {
          console.log('Video ad open.');
        },
        onRewarded: () => {
          console.log('Rewarded!');
          myGameInstance.SendMessage('Yandex', 'AddCoins', value);
        },
        onClose: () => {
          console.log('Video ad closed.');
        }, 
        onError: (e) => {
          console.log('Error while open video ad:', e);
        }
    }
})
  },

});