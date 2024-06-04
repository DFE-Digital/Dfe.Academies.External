class DataGenerator {
  generateFAMTrustOpeningDate() {
    const today = new Date()
    const yearDate = new Date().getFullYear()

    if (today.getMonth() > 7) {
      return yearDate + 1
    }
    else {
      return yearDate
    }
  }
}

export default DataGenerator
