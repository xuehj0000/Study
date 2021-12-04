Vue.component("top",{
    template : `<div id="header">
                    <img>
                    <div class="user-login" v-if="username==null||username==''">
                        <span @click="showLoginPad">登录</span>
                        <span>注册</span>
                    </div>
                    <div class="user-login" v-else>
                        <span>{{username}}</span>
                        <span>注销</span>
                    </div>
                </div>
    `,
    props:["username"],
    methods:{
        showLoginPad(){
            this.$emit("showloginpad",true);
        }
    }
})