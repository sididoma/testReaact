import React, { Component } from 'react';
import { Redirect } from 'react-router-dom';
export class Questionnaire extends Component {

    constructor(props) {
        super(props);
        this.state = {
            questions: [],
            loading: true,
            current: 0,
            size: 0,
            isFirst: true,
            isLast: false,
        };
        this.prevQuestion = this.prevQuestion.bind(this);
        this.nextQuestion = this.nextQuestion.bind(this);
        this.handleChange = this.handleChange.bind(this);
    }

    componentDidMount() {
        this.fetchQuestions();
    }

    async fetchQuestions() {
        const response = await fetch('Questionnaire');
        const data = await response.json();
        this.setState({
            questions: data,
            loading: false,
            size: data.length,
        });
    }

    render() {
        let content = this.state.loading ? <p><em>Loading...</em></p> : (this.renderForm());

        return (
            <div>
                {content}
            </div>
        );
    }

    renderForm() {
        const curr = this.state.current;
        return (<QForm isFirst={curr === 0} isLast={this.state.size - curr === 1}
            inputType={this.state.questions[curr].type}
            inputValue={this.state.questions[curr].answer}
            enums={this.state.questions[curr].enumDescriptions}
            prevQuestion={(() => this.prevQuestion())} nextQuestion={(() => this.nextQuestion())}
            tagName={this.state.questions[curr].question}
            changehandler={this.handleChange}
        ></QForm>);
    }
    prevQuestion() {
        this.setState({
            current: this.state.current - 1
        });
    }
    nextQuestion() {
        if (this.state.current == this.state.size - 1) {
            const requestBody = this.state.questions;
            const requestOptions = {
                method: 'POST',
                headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(requestBody)
            };
            fetch('questionnaire/SaveAnswers', requestOptions)
                .then(async response => {
                    const data = await response.json();
                    if (!response.ok) {
                        const error = (data && data.message) || response.status;
                        return Promise.reject(error);
                    }
                    alert('success');
                    window.location.href = '/';
                })
                .catch(error => {

                    alert('Error while sending data to bd: ' + error.toString());
                })

        } else {
            this.setState({
                current: this.state.current + 1,
            });
        }
    }
    handleChange(event) {
        let quests = this.state.questions
        quests[this.state.current].answer = String(event.target.value);
        this.setState({
            questions: quests,
        })
    }
}

class QForm extends Component {
    render() {
        return (
            <div>
                <form>
                    <QuestionInput type={this.props.inputType} value={this.props.inputValue} enums={this.props.enums} tagName={this.props.tagName} handlechange={this.props.changehandler}></QuestionInput>
                    <input type='button' disabled={this.props.isFirst} onClick={this.props.prevQuestion} value='Назад'></input>
                    <input type='button' onClick={this.props.nextQuestion} value={this.props.isLast ? "Закончить" : "Следующий вопрос"}></input>
                </form>
            </div>
        )
    }
}

function QuestionInput(props) {
    if (props.type === 'int') {
        return (<NumberInput value={props.value} handlechange={props.handlechange} tagName={props.tagName}></NumberInput>);
    }
    if (props.type === 'date') {
        return (<DateInput value={props.value} handlechange={props.handlechange} tagName={props.tagName}></DateInput>)
    }
    if (props.type === 'enum') {
        return (<SelectListInput value={props.value} handlechange={props.handlechange} data={props.enums} tagName={props.tagName}></SelectListInput>)
    }
    if (props.type === 'bool') {
        return (<CheckBoxInput value={props.value} handlechange={props.handlechange} tagName={props.tagName}></CheckBoxInput>)
    }
    return (<StringInput value={props.value} handlechange={props.handlechange} tagName={props.tagName}></StringInput>)

}

function NumberInput(props) {
    return (<div>
        {props.tagName}
        <input type='number' value={props.value} onChange={props.handlechange}></input>
    </div>
    );
}
function StringInput(props) {
    console.log(props.state)
    return (<div>
        {props.tagName}
        <input type='text' value={props.value} onChange={props.handlechange}></input>
    </div>
    );
}
function DateInput(props) {
    return (<div>
        {props.tagName}
        <input type='date' value={props.value} onChange={props.handlechange}></input>
    </div>
    );
}
function SelectListInput(props) {
    return (<div>
        {props.tagName}
        <select value={props.value} onChange={props.handlechange}>
            {props.data.map(curr =>
                <option value={curr}>{curr}</option>)}
        </select>
    </div>)
}
function CheckBoxInput(props) {
    return (<div>
        {props.tagName}
        <input type='checkbox' value={props.value} onChange={props.handlechange}></input>
    </div>)
}