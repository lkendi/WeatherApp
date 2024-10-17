function filterForecast(day) {
    const cards = document.querySelectorAll('.forecast-card');
    const buttons = document.querySelectorAll('.day-filter-btn');

    cards.forEach(card => {
        const forecastDay = card.getAttribute('data-day');
        if (forecastDay === day || (day === 'next-5-days' && forecastDay !== 'today')) {
            card.style.display = 'block';
        } else {
            card.style.display = 'none';
        }
    });

    buttons.forEach(button => {
        button.classList.remove('active');
        if (button.innerText.toLowerCase() === day.toLowerCase()) {
            button.classList.add('active');
            button.style.backgroundColor = 'white'; 
            button.style.color = 'black';
        } else {
            button.style.backgroundColor = '';
            button.style.color = ''; 
        }
    });
}
