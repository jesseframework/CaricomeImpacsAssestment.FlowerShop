$(function () {
    abp.log.debug('Index.js initialized!');
    

    
});




function updateCartDisplay(itemCount, totalCost) {
    document.getElementById('itemCountBadge').innerText = itemCount;
    document.getElementById('totalCostBadge').innerText = `$${totalCost.toFixed(2)}`;
}

function updateCartDisplay(itemCount, totalCost) {
    const itemCountBadge = document.getElementById('itemCountBadge');
    const totalCostBadge = document.getElementById('totalCostBadge');

    if (itemCount > 0) {
        itemCountBadge.style.display = 'inline';
        totalCostBadge.style.display = 'inline';
        itemCountBadge.innerText = itemCount;
        totalCostBadge.innerText = `$${totalCost.toFixed(2)}`;
    } else {
        itemCountBadge.style.display = 'none';
        totalCostBadge.style.display = 'none';
    }
}

