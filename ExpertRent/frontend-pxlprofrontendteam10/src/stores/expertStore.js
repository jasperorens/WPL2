import {defineStore} from 'pinia'

export const useExpertStore = defineStore('expert', {
    state: () => ({
        users: [],
        experts: [],
        userExperts: [],
    }),
    actions: {
         assignAllUsers() {
            try {
                fetch("https://localhost:7155/api/User/GetUsers", {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                    },
                }).then(response =>{
                    return response.json()
                }).then(data=>{
                    this.users = JSON.parse(data)
                })
                
            } catch (error) {
                console.error(error)
            }
        },

        assignUserExperts() {
            try {
                fetch('https://localhost:7155/api/Expert/GetAllExpertsIfUser', {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                    },
                }).then(response =>{
                    return response.json()
                }).then(data=>{
                    this.userExperts = JSON.parse(data)
                })

            } catch (error) {
                console.error(error)
            }
        },
        
        assignAllExperts() {
            try {
                fetch("https://localhost:7155/api/Expert/GetExperts", {
                    method: 'GET',
                    headers: {
                        'Accept': 'application/json',
                    },
                }).then(response =>{
                    return response.json()
                }).then(data=>{
                    this.experts = JSON.parse(data)
                    //console.log(this.experts)
                })


            } catch (error) {
                console.error(error)
            }

        },

        isUserExpert(name) {
            const user = this.experts.find((u) => u.username === name);
            /*if(user) {
                return true;
            } else {
                return false;
            }*/
            return !!user;
        }
    },
    getters: {
        getUsers() {
            return this.users
        },
        
        getExpert() {
            return this.experts
        },
        
        getUserExpert() {
            return this.userExperts
        }
    }
})
