import React, { Component } from 'react';

export class PreDefinedRules extends Component {
  static displayName = PreDefinedRules.name;

  constructor(props) {
    super(props);
    this.state = { numbers: [], loading: true };
  }

  componentDidMount() {
    // this.populateWeatherData();
  }

  static renderNumbersTable(numbers) {
    let key = 1
    return (
      <table className='table table-striped' aria-labelledby="tabelLabel">
        <thead>
          <tr>
            <th>Number</th>
            <th>Processed Numbers</th>
          </tr>
        </thead>
        <tbody>
          {numbers.map(num =>
            <tr key={key++}>
              <td>{num.number}</td>
              <td>{num.word}</td>
            </tr>
          )}
        </tbody>
      </table>
    );
  }

  render() {
    let contents = this.state.loading
      ? <p><em>No data</em></p>
      : PreDefinedRules.renderNumbersTable(this.state.numbers);

    return (
      <div className='container'>
        <h1 id="tabelLabel" >Numbers</h1>
        <p>This component demonstrates fetching data from the server.</p>
        <div className='card bg-light mb-3'>
          <div className='card-header'>Actions</div>
          <div className='card-body'>
            <button onClick={() => this.populateFizzBuzzData()} className='btn btn-primary'>Generate FizzBuzz</button>&nbsp;
            <button onClick={() => this.populateFizzBuzzJazzFuzzData()} className='btn btn-primary'>Generate FizzBuzzJazzFuzz</button>
          </div>
        </div>
        <div className='card bg-light mb-3'>
          <div className='card-header'>Data</div>
          <div className='card-body'>
            {contents}
          </div>
        </div>
      </div>
    );
  }

  async fetchData(urls) {
    const result = await Promise.all(urls.map(url => fetch(url)))
      .then(async (res) => {
        return Promise.all(
          res.map(async (data) => {
            const arr = await data.json();
            return arr;
          })
        )
      }).catch(error => {
        console.log("Error", error);
      });
    console.log(result)
    return result.flatMap(data => data);
  }

  async populateFizzBuzzData() {
    this.setState({ numbers: [], loading: true });
    const response = await fetch('numbers/fizzbuzz-full?from=1&to=100');
    const data = await response.json();
    this.setState({ numbers: data, loading: false });
  }

  async populateFizzBuzzJazzFuzzData() {
    this.setState({ numbers: [], loading: true });
    const data = await this.fetchData(['numbers/fizzbuzz-full?from=1&to=100', 'numbers/jazzfuzz-full?from=100&to=1'])
    this.setState({ numbers: data, loading: false });
  }
}
