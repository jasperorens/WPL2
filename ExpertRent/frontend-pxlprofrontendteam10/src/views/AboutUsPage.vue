<template>

    <div className="about">

        <div className="whiteContainer">
            <h1 className="Head">About us</h1>
            <p>
                We are a dynamic and innovative company created by a group of students at PXL.
                Our company specializes in offering expert advice and services
                to our customers by renting the skills of talented individuals.
                We believe that everyone has a unique talent and expertise,
                and we provide a platform for people to showcase and monetize
                their skills. Our goal is to connect customers with the right
                experts to meet their specific needs. We take pride in our ability
                to deliver quality service and exceptional value to our customers.
                At our core, we are a team of passionate individuals committed to helping
                people succeed.
            </p>


            <div className="groupBox">
                <img className="picture" src="/src/assets/groep.jpg" alt="Foto placeholder">
                <div className="whiteContainer2">
                    <p>If you wish to contact us you can do so here</p>
                    <router-link to="/contact" className="navButton">
                        <p className="Head2">contact</p>
                    </router-link>
                </div>
            </div>

        </div>

    </div>

</template>

<script>
export default {
    name: 'AboutUsPage'
};
</script>


<style scoped>

.about {
    display: flex;
    flex-direction: column;
}

.whiteContainer {
    background-color: white;
    display: flex;
    flex-direction: column;
    margin: 50px 10%;
    border-radius: 20px;
    padding: 10px 11%;
}

.whiteContainer2 {
    background-color: #B6D3D7;
    display: flex;
    flex-direction: column;
    margin: 10px 20px;
    border-radius: 20px;
    padding: 20px 20px;
    height: 100%;
}

p {
    font-family: Bahnschrift, serif;
}

.groupBox {
    display: flex;
    flex-direction: column;
    justify-content: center;
}

.Head {
    padding: 10px 30px;
    background-color: #2A93BC;
    color: white;
    display: flex;
    justify-content: center;
    align-self: center;
    border-radius: 10px;
    width: 95%;
}

.Head2 {
    padding: 10px;
    background-color: #2A93BC;
    color: white;
    border-radius: 10px;
    text-align: center;
}

.picture {
    width: 100%;
    border-radius: 20px;
    display: flex;
    align-self: center;
    align-items: center;
    padding: 10px;
}

.whiteContainer2 p {
    text-align: center;
}

/*/////////////////web mods///////////////////////*/
@media screen and (min-width: 880px) {
    .groupBox {
        display: flex;
        flex-direction: row;
        justify-content: center;
    }

    .whiteContainer {
        padding: 0 11%;
    }

    .picture {
        width: 500px;
    }

}
</style>
