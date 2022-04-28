function showErrors(validationDictString, operation) {
    // Debug
    console.log('ShowErrorS triggered for ' + validationDictString)
    
    // Convert the JSON dict to a JS dict
    const validationDict = JSON.parse(validationDictString)
    
    // Loop over JSON object and trigger errors where required
    for (const key of Object.keys(validationDict)) {
        if(!validationDict[key]) {
            this.showErrorFor(key.replaceAll(" ", ""), operation)
        }
    }
}

function showErrorFor(inputName, operation) {
    // Debug
    console.log('ShowError (single) triggered for ' + inputName + ', ' + operation)
    
    // Grab elements
    const span = document.getElementById("Span" + operation + inputName);
    const input = document.getElementById("Input" + operation + inputName);
    const error = document.getElementById("Error" + operation + inputName);
    
    // Show errors
    span.classList.add('border-danger')
    input.classList.add('border-danger')
    error.classList.add('d-inline')
    error.classList.add('sticky-top')
    
}

function hideErrors(operation) {
    // Debug
    console.log('HideErrors triggered for ' + operation)
    
    // Grab elements
    const SpanJobName = document.getElementById('Span' + operation + 'JobName');
    const InputJobName = document.getElementById('Input' + operation + 'JobName');
    const ErrorJobName = document.getElementById('Error' + operation + 'JobName');

    const SpanTargets = document.getElementById('Span' + operation + 'Targets');
    const InputTargets = document.getElementById('Input' + operation + 'Targets');
    const ErrorTargets = document.getElementById('Error' + operation + 'Targets');

    const SpanLabels = document.getElementById('Span' + operation + 'Labels');
    const InputLabels = document.getElementById('Input' + operation + 'Labels');
    const ErrorLabels = document.getElementById('Error' + operation + 'Labels');

    const SpanScrapeInterval = document.getElementById('Span' + operation + 'ScrapeInterval');
    const InputScrapeInterval = document.getElementById('Input' + operation + 'ScrapeInterval');
    const ErrorScrapeInterval = document.getElementById('Error' + operation + 'ScrapeInterval');
    
    const SpanScrapeTimeout = document.getElementById('Span' + operation + 'ScrapeTimeout');
    const InputScrapeTimeout = document.getElementById('Input' + operation + 'ScrapeTimeout');
    const ErrorScrapeTimeout = document.getElementById('Error' + operation + 'ScrapeTimeout');

    const SpanMetricsPath = document.getElementById('Span' + operation + 'MetricsPath');
    const InputMetricsPath = document.getElementById('Input' + operation + 'MetricsPath');
    const ErrorMetricsPath = document.getElementById('Error' + operation + 'MetricsPath');
    
    // Fill collections
    let borders = [
        SpanJobName, InputJobName, SpanTargets, InputTargets, SpanLabels, InputLabels,
        SpanScrapeInterval, InputScrapeInterval, SpanScrapeTimeout, InputScrapeTimeout,
        SpanMetricsPath, InputMetricsPath
    ];
    
    let displays = [
        ErrorJobName, ErrorTargets, ErrorLabels, ErrorScrapeInterval, ErrorScrapeTimeout, ErrorMetricsPath
    ];
    
    
    // Remove errors
    borders.forEach(function(item, index, array) {
        try {
            item.classList.remove('border-danger');
        } catch (e) {
            // Do nothing
        }
        
    });
    
    console.log('wtf?')
    displays.forEach(function(item, index, array) {
        try {
            item.classList.remove('d-inline');
            item.classList.remove('sticky-top');
        } catch (e) {
            // Do nothing
        }
    })
}