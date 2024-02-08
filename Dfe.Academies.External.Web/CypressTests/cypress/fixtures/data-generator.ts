class DataGenerator {

    generateFAMTrustOpeningDate() {
        const today = new Date()
        let yearDate = new Date().getFullYear()

        if (today.getMonth() > 7)
        {
        yearDate = yearDate + 1
        }
        else
        {
            yearDate = yearDate
        }
        return yearDate
    }

}

export default DataGenerator