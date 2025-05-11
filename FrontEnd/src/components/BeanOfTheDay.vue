<script setup lang="ts">
</script>

<template>
    <div class="bean-of-the-day" >
        <h1 class="coffee-brown">Bean of the Day</h1>
        <img class="botd-img" src="https://example.com/bean-of-the-day.jpg" alt="botd-image" />
        <h1 class="botd-name">botd-name</h1>
        <div class="botd-description">
            <p>botd-description</p>
        </div>
        <div class="botd-details">
            <p>botd-country</p>
            <p>botd-price</p>
            <p>botd-colour</p>
        </div>
    </div>
</template>

<style scoped>
.bean-of-the-day {
    text-align: center;
    margin: 20px;
}

.botd-img {
    border: 2px solid var(--color-accent);;
    border-radius: 20px;
}

.botd-name {
    font-size: 2rem;
    margin: 10px 0;
}
.botd-description {
    font-size: 1.2rem;
    margin: 10px 0;
}
.botd-details {
    width: 100%;
    padding: 0 10% 0 10%;
    justify-content: space-between;
    display: flex;
    flex-flow: row;
}
</style>