// pages/productManager/productManager.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    products:[]
  },
  /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.getProducts();
  },
  getProducts(){
    wx.request({
      url: 'http://127.0.0.1:5916/product/Product',
      success:res=>{
        this.setData({products:res.data});
      }
    })
  },
})