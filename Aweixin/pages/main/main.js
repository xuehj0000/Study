// pages/main/main.js
Page({

  /**
   * 页面的初始数据
   */
  data: {

  },
  goList:function(e){
    var {type,bus} = e.currentTarget.dataset;
    switch(type){
      case "user":
        wx.navigateTo({ url: '../userManager/userManager'})
        break;
        case "product":
          wx.navigateTo({ url: 'url'})
        break;
    }
  },

  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {

  },
})