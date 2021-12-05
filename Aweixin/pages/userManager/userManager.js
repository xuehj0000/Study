// pages/userManager/userManager.js
Page({

  /**
   * 页面的初始数据
   */
  data: {
    users:[]
  },
  searchText:"",
    /**
   * 生命周期函数--监听页面加载
   */
  onLoad: function (options) {
    this.getUsers();
  },
  getUsers:function(){
    var that = this;
    wx.request({url:'http://127.0.0.1:5916/user/users',success(res){
      console.log(res);
      that.users = res.data;
      that.setData({users:res.data});
    }})
  },
  search:function(){
    var that = this;
    wx.request({url:'http://127.0.0.1:5916/user/users/' + this.searchText,success(res){
      that.users = res.data;
      that.setData({users:res.data});
    }})
  },
  textChange:function(e){
    var {value} = e.detail;
    this.searchText = value;
  }

})