<div class="w-full md:w-6/12 md:px-10 rounded-3xl titleDiv ">
    <div class="advertGrid text-red-600 ">
        <div class="title text-center">
            <p>ThesisRestaurant</p>
        </div>
        <div class="description">
            <p>
                Looking for a slice of heaven? <br />
                Visit ThesisRestaurant today and try our amazing pizzas baked in
                a wood-fired oven. <br />
                Don’t miss this opportunity to savor a cheesy delight at ThesisRestaurant!
            </p>
        </div>
        <div class="foot">
            <p>Call us: 06-30-111-1111</p>
            <p>
                Write us an email: <a
                    class="underline "
                    href="mailto:thesisrestaurant@gmail.com"
                    >thesisrestaurant@gmail.com</a
                >
            </p>
            <p />
        </div>
    </div>
</div>

<style>
    .titleDiv {
        position: relative;
    }
    .advertGrid:before {
        border-top-left-radius: 1.5rem;
        border-bottom-left-radius: 1.5rem;
        content: "";
        background-image: url("https://images.unsplash.com/photo-1621538997517-58ce53933faa?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80");
        /* background-image: url('https://placekitten.com/1200/800'); */
        background-size: cover;
        position: absolute;
        top: 0px;
        right: 0px;
        bottom: 0px;
        left: 0px;
        width: 100%;
        height: 100%;
        opacity: 0.3;
    }
    .advertGrid {
        width: 100%;
        z-index: 5;
        opacity: 1 !important;
        display: grid;
        grid-template-rows: repeat(12, 1fr);
    }
    .title {
        font-size: 60px;
        grid-row: 3;
    }
    .description {
        font-size: 30px;
        grid-row: 5 / span 4;
    }
    .foot {
        font-size: 20px;
        grid-row: 9 / span 2;
    }
</style>
